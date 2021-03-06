using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Film
{
	/// <summary>
	/// Model representing data retrieved from the Film resource.
	/// </summary>
	public class FilmSummary
	{
		public string Title { get; set; }

		[JsonProperty("episode_id")]
		public int EpisodeId { get; set; }

		[JsonProperty("opening_crawl")]
		public string OpeningCrawl { get; set; }
		public string Director { get; set; }
		public string Producer { get; set; }

		[JsonProperty("release_date")]
		public string ReleaseDate { get; set; }
		public List<string> Characters { get; set; }
		public List<string> Planets { get; set; }
		public List<string> Starships { get; set; }
		public List<string> Vehicles { get; set; }
		public List<string> Species { get; set; }
		public string Created { get; set; }
		public string Edited { get; set; }
		public string Url { get; set; }
	}
}
