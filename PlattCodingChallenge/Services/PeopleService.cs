using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models;
using PlattCodingChallenge.Models.People;
using PlattCodingChallenge.Models.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with people data.
	/// </summary>
	public class PeopleService : SWApiServiceBase, IPeopleService
	{
		#region Fields
		private readonly IPlanetService _planetService;
		#endregion

		#region Ctor(s)
		public PeopleService(ILogger<PeopleService> logger, IHttpClientFactory httpClientFactory, IPlanetService planetService) : base(logger, httpClientFactory)
		{
			_planetService = planetService;
		}
		#endregion

		#region Public Methods
		public async Task<PlanetResidentsViewModel> GetPlanetResidentsViewModelAsync(string planetName)
		{
			PlanetResidentsViewModel planetResidentsViewModel = null;
			ResidentSummary[] residentSummaries = await GetResidentSummariesByPlanetNameAsync(planetName);

			if (residentSummaries != null)
			{
				planetResidentsViewModel = new PlanetResidentsViewModel();

				foreach (ResidentSummary resident in residentSummaries)
				{
					planetResidentsViewModel.Residents.Add(new ResidentDetailsViewModel(resident));
				}

				// sort by name alphabetically, ascending.
				planetResidentsViewModel.Residents = planetResidentsViewModel.Residents.OrderBy(x => x.Name).ToList();
			}

			return planetResidentsViewModel;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Attempts to retrieve Resident information based on a specified planet.
		/// </summary>
		/// <param name="planetName">The name of the planet to get resident data for.</param>
		/// <returns><see cref="ResidentSummary"/>[]</returns>
		private async Task<ResidentSummary[]> GetResidentSummariesByPlanetNameAsync(string planetName)
		{
			ResidentSummary[] residentSummaries = null;
			PlanetSummary planetSummary = await _planetService.GetPlanetSummaryByNameAsync(planetName);
			List<Task<ResidentSummary>> residentTasks = new List<Task<ResidentSummary>>();

			if (planetSummary != null)
			{
				// parses the list of resident endpoints to get the ID's off of the end of the url via Regex as a list
				IEnumerable<int> residentIds = planetSummary.Residents.Select(x => int.Parse(_matchId.Match(x).Value));

				foreach (int residentId in residentIds)
				{
					// this could potentially be a lot of requests, perform these asynchronously for better performance.
					residentTasks.Add(GetResidentSummaryByIdAsync(residentId));
				}

				// wait until all of the resident requests are done
				residentSummaries = await Task.WhenAll(residentTasks.ToArray());
			}

			return residentSummaries;
		}

		/// <summary>
		/// Gets the <see cref="ResidentSummary"/> for a given residentId
		/// </summary>
		/// <param name="residentId">The ID of the resident to get a summary for.</param>
		/// <returns><see cref="ResidentSummary"/></returns>
		private async Task<ResidentSummary> GetResidentSummaryByIdAsync(int residentId)
		{
			ResidentSummary residentSummary = null;
			string targetUri = $"api/people/{residentId}/";
			HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

			if (response.IsSuccessStatusCode)
			{
				string rawJson = await response.Content.ReadAsStringAsync();
				residentSummary = JsonConvert.DeserializeObject<ResidentSummary>(rawJson);
			}
			else
			{
				_logger.LogError($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
			}

			return residentSummary;
		}
		#endregion
	}
}
