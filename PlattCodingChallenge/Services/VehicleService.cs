using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models;
using PlattCodingChallenge.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with vehicle data.
	/// </summary>
	public class VehicleService : SWApiServiceBase, IVehicleService
	{
		#region Ctor(s)
		public VehicleService(ILogger<VehicleService> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
		{
		}
		#endregion

		#region Public Methods
		public async Task<VehicleSummaryViewModel> GetVehicleSummaryViewModelAsync()
		{
			VehicleSummaryViewModel vehicleSummaryViewModel = null;

			try
			{
				List<VehicleSummary> vehicleSummaries = await GetVehicleSummariesAsync();

				if (vehicleSummaries != null)
				{
					// Filter out any entries that do not have valid CostInCredits values
					IEnumerable<VehicleSummary> filteredSummaries = vehicleSummaries.Where(x => int.TryParse(x.CostInCredits, out int y));
					IEnumerable<string> distinctManufacturers = vehicleSummaries.Select(v => v.Manufacturer).Distinct();
					List<VehicleStatsViewModel> detailsList = new List<VehicleStatsViewModel>();
					vehicleSummaryViewModel = new VehicleSummaryViewModel()
					{
						ManufacturerCount = distinctManufacturers.Count(),
						VehicleCount = vehicleSummaries.Count
					};

					foreach (string manufacturer in distinctManufacturers)
					{
						// Get a collection of vehicle summaries, filtered by manufacturer
						IEnumerable<VehicleSummary> matchedSummaries = vehicleSummaries.Where(vs => vs.Manufacturer == manufacturer);

						// ensure we got a result set back, and that the CostInCredits property has at least one numerical value.
						if (matchedSummaries?.Count() > 0 && matchedSummaries.All(ms => int.TryParse(ms.CostInCredits, out int cost) == false) == false)
						{
							int cost = 0;
							double averageCost = matchedSummaries.Where(ms => int.TryParse(ms.CostInCredits, out cost)).Select(x => cost).Average();
							detailsList.Add(new VehicleStatsViewModel()
							{
								ManufacturerName = manufacturer,
								AverageCost = averageCost,
								VehicleCount = matchedSummaries.Count()
							});
						}
					}

					vehicleSummaryViewModel.Details = detailsList.OrderByDescending(x => x.VehicleCount).ThenByDescending(x => x.AverageCost).ToList();
				}
			}
			catch (Exception ex)
			{
				// Something bad happened. Log the result and fail out.
				_logger.LogError(ex, ex.Message, null);
			}

			return vehicleSummaryViewModel;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Attempts to retreive a list of <see cref="VehicleSummary"/> from the data source.
		/// </summary>
		/// <returns><see cref="VehicleSummary"/></returns>
		private async Task<List<VehicleSummary>> GetVehicleSummariesAsync()
		{
			List<VehicleSummary> vehicleSummaries = null;
			string targetUri = "/api/vehicles/";

			try
			{
				// Get the first page of results
				HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

				if (response.IsSuccessStatusCode)
				{
					string rawJson = await response.Content.ReadAsStringAsync();
					PaginatedVehicleResponse paginatedResponse = JsonConvert.DeserializeObject<PaginatedVehicleResponse>(rawJson);
					vehicleSummaries = new List<VehicleSummary>();
					vehicleSummaries.AddRange(paginatedResponse.Results);

					// Keep looping over the paginated responses until there are no pages left to load.
					while (string.IsNullOrWhiteSpace(paginatedResponse.Next) == false)
					{
						string nextPageUri = $"{targetUri}?page={paginatedResponse.GetNextPageId()}";
						response = await _httpClient.GetAsync(nextPageUri);

						if (response.IsSuccessStatusCode)
						{
							rawJson = await response.Content.ReadAsStringAsync();
							paginatedResponse = JsonConvert.DeserializeObject<PaginatedVehicleResponse>(rawJson);
							vehicleSummaries.AddRange(paginatedResponse.Results);
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

			return vehicleSummaries;
		}
		#endregion
	}
}
