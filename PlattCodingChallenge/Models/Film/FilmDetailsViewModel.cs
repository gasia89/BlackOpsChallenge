using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Film
{
	/// <summary>
	/// Model for displaying film data.
	/// </summary>
	public class FilmDetailsViewModel
	{
		public int EpisodeId { get; set; }
		public string Title { get; set; }
		public IEnumerable<int> StarshipIds { get; set; }
	}
}
