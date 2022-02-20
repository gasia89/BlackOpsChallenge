using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models.Film;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with Film data.
	/// </summary>
	public class FilmService : SWApiServiceBase, IFilmService
	{
		#region Ctor(s)
		public FilmService(ILogger<FilmService> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
		{
		}
		#endregion

		#region Public Methods
		public async Task<FilmDetailsViewModel> GetFilmDetailsViewModelAsync(int episodeId)
		{
			int starshipId = 0;
			FilmSummary filmSummary = await GetFilmSummaryByEpisodeIdAsync(episodeId);
			FilmDetailsViewModel filmDetailsViewModel = new FilmDetailsViewModel()
			{
				EpisodeId = filmSummary.EpisodeId,
				Title = filmSummary.Title,
				StarshipIds = filmSummary.Starships.Select(x =>
				{
					int.TryParse(_matchId.Match(x).Value, out starshipId);
					return starshipId;
				})
			};

			return filmDetailsViewModel;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets a <see cref="FilmSummary"/> for the supplied episodeId.
		/// </summary>
		/// <param name="episodeId">The id of the Episode to get data for.</param>
		/// <returns><see cref="FilmSummary"/></returns>
		private async Task<FilmSummary> GetFilmSummaryByEpisodeIdAsync(int episodeId)
		{
			FilmSummary filmSummary = null;
			string targetUri = $"api/films/{episodeId}/";
			HttpResponseMessage response = await _httpClient.GetAsync(targetUri);

			if (response.IsSuccessStatusCode)
			{
				string rawJson = await response.Content.ReadAsStringAsync();
				filmSummary = JsonConvert.DeserializeObject<FilmSummary>(rawJson);
			}
			else
			{
				_logger.LogError($"Request Failed, StatusCode: {response.StatusCode}, Endpoint: {_httpClient.BaseAddress}{targetUri}");
			}

			return filmSummary;
		}
		#endregion
	}
}
