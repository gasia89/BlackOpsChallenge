using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PlattCodingChallenge.Configuration;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models;
using PlattCodingChallenge.Models.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with Planet data.
	/// </summary>
	public class PlanetService : SWApiServiceBase, IPlanetService
	{
		private readonly PlanetSettings _planetSettings;
		#region Ctor(s)
		public PlanetService(ILogger<PlanetService> logger, IHttpClientFactory httpClientFactory, IOptions<PlanetSettings> planetOptions) : base(logger, httpClientFactory)
		{
			_planetSettings = planetOptions.Value;
		}
		#endregion

		#region Public Methods
		public async Task<AllPlanetsViewModel> GetAllPlanetsViewModelAsync()
		{
			List<PlanetDetailsViewModel> planetViewModels = await GetAllPlanetDetailsViewModelsAsync();
			AllPlanetsViewModel allPlanetsVm = new AllPlanetsViewModel()
			{
				Planets = planetViewModels,
				AverageDiameter = CalculateAverageDiameter(planetViewModels)
			};

			return allPlanetsVm;
		}

		public async Task<SinglePlanetViewModel> GetSinglePlanetViewModelAsync(int planetId)
		{
			PlanetSummary planetSummary = await GetPlanetSummaryByIdAsync(planetId);
			SinglePlanetViewModel planetVM = new SinglePlanetViewModel(planetSummary);

			return planetVM;
		}

		public async Task<List<PlanetSummary>> GetPlanetSummariesAsync()
		{
			List<PlanetSummary> planetSummaries = null;
			string targetUri = "/api/planets/";

			try
			{
				// Get the first page of results
				HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

				if (response.IsSuccessStatusCode)
				{
					string rawJson = await response.Content.ReadAsStringAsync();
					PaginatedPlanetResponse paginatedResponse = JsonConvert.DeserializeObject<PaginatedPlanetResponse>(rawJson);
					planetSummaries = new List<PlanetSummary>();
					planetSummaries.AddRange(paginatedResponse.Results);

					// Keep looping over the paginated responses until there are no pages left to load.
					while (string.IsNullOrWhiteSpace(paginatedResponse.Next) == false)
					{
						string nextPageUri = $"{targetUri}?page={paginatedResponse.GetNextPageId()}";
						response = await _httpClient.GetAsync(nextPageUri);

						if (response.IsSuccessStatusCode)
						{
							rawJson = await response.Content.ReadAsStringAsync();
							paginatedResponse = JsonConvert.DeserializeObject<PaginatedPlanetResponse>(rawJson);
							planetSummaries.AddRange(paginatedResponse.Results);
						}
						else
						{
							throw new Exception($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
						}
					}
				}
				else
				{
					throw new Exception($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message, null);
			}

			return planetSummaries;
		}

		public async Task<PlanetSummary> GetPlanetSummaryByNameAsync(string planetName)
		{
			PlanetSummary planetSummary = null;
			string targetUri = $"api/planets/?search={planetName}";
			HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

			if (response.IsSuccessStatusCode)
			{
				string rawJson = await response.Content.ReadAsStringAsync();
				PaginatedPlanetResponse paginatedPlanetResponse = JsonConvert.DeserializeObject<PaginatedPlanetResponse>(rawJson);

				if(paginatedPlanetResponse?.Results.Count > 0)
				{
					planetSummary = paginatedPlanetResponse.Results[0];
				}
			}
			else
			{
				_logger.LogError($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
			}

			return planetSummary;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets all <see cref="PlanetDetailsViewModel"/>'s for all available planets
		/// </summary>
		/// <returns>List of <see cref="PlanetDetailsViewModel"/></returns>
		private async Task<List<PlanetDetailsViewModel>> GetAllPlanetDetailsViewModelsAsync()
		{
			List<PlanetSummary> planetSummaries = await GetPlanetSummariesAsync();
			List<PlanetDetailsViewModel> orderedDetailViewModels;
			List<PlanetDetailsViewModel> detailsViewModelsWithoutDiameterValues = new List<PlanetDetailsViewModel>();
			List<PlanetDetailsViewModel> detailsViewModelsWithDiameterValues = new List<PlanetDetailsViewModel>();
			List<int> planetsDiameters = new List<int>();

			foreach (PlanetSummary planetSummary in planetSummaries)
			{
				PlanetDetailsViewModel detailsViewModel = new PlanetDetailsViewModel(planetSummary);

				// Check to make sure we have a valid diameter
				if (int.TryParse(planetSummary.Diameter, out int parsedDiameter))
				{
					detailsViewModelsWithDiameterValues.Add(detailsViewModel);
				}
				else
				{
					detailsViewModelsWithoutDiameterValues.Add(detailsViewModel);
				}
			}

			orderedDetailViewModels = detailsViewModelsWithDiameterValues.OrderByDescending(x => int.Parse(x.Diameter)).ToList();
			// append the results without valid diameters to the end of the list.
			orderedDetailViewModels.AddRange(detailsViewModelsWithoutDiameterValues);

			return orderedDetailViewModels;
		}

		/// <summary>
		/// Gets the <see cref="PlanetSummary"/> for a given planetId
		/// </summary>
		/// <param name="planetId">The ID of the planet to get a summary for.</param>
		/// <returns><see cref="PlanetSummary"/></returns>
		private async Task<PlanetSummary> GetPlanetSummaryByIdAsync(int planetId)
		{
			PlanetSummary planetSummary = null;
			string targetUri = $"api/planets/{planetId}/";
			HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

			if (response.IsSuccessStatusCode)
			{
				string rawJson = await response.Content.ReadAsStringAsync();
				planetSummary = JsonConvert.DeserializeObject<PlanetSummary>(rawJson);
			}
			else
			{
				_logger.LogError($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
			}

			return planetSummary;
		}

		/// <summary>
		/// Calculates the averate diameter of a collection of <see cref="PlanetDetailsViewModel"/>'s.
		/// </summary>
		/// <param name="planets">The planets to calculate the average diameter of.</param>
		/// <returns>The average diameter for the given collection of planets</returns>
		private double CalculateAverageDiameter(IEnumerable<PlanetDetailsViewModel> planets)
		{
			IEnumerable<int> diameters;
			int diameter = 0;
			long totalDiameterOfCalculatedPlanets;

			// I noticed some of the planets have a zero diameter. This skews the results.
			// However, the instructions do not say to filter out zero values, just the "unknown" values.
			// Since I was not sure, I setup a config key to include/exclude them as desired.
			if (_planetSettings.IncludeZeroDiameterPlanetsInAverage)
			{
				diameters = planets.Where(x => int.TryParse(x.Diameter, out diameter)).Select(x => diameter);
			}
			else
			{
				// filter out zero diameter results from the average.
				diameters = planets.Where(x => int.TryParse(x.Diameter, out diameter) && x.Diameter != "0").Select(x => diameter);
			}

			totalDiameterOfCalculatedPlanets = diameters.Sum();

			foreach (int currentDiameter in diameters)
			{
				totalDiameterOfCalculatedPlanets += currentDiameter;
			}

			return totalDiameterOfCalculatedPlanets / (double)diameters.Count();
		}
		#endregion
	}
}
