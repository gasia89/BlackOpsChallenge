using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models.Starship;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with Starship data.
	/// </summary>
	public class StarshipService : SWApiServiceBase, IStarshipService
	{
		#region Ctor(s)
		public StarshipService(ILogger<StarshipService> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
		{
		}
		#endregion

		#region Public Methods
		public async Task<StarshipSummary> GetStarshipSummaryByIdAsync(int starshipId)
		{
			StarshipSummary starshipSummary = null;
			string targetUri = $"/api/starships/{starshipId}";

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

				if (response.IsSuccessStatusCode)
				{
					string rawJson = await response.Content.ReadAsStringAsync();
					starshipSummary = JsonConvert.DeserializeObject<StarshipSummary>(rawJson);
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

			return starshipSummary;
		}
		#endregion
	}
}
