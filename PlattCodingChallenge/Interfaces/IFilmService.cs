using PlattCodingChallenge.Models.Film;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with Film data.
	/// </summary>
	public interface IFilmService
	{
		/// <summary>
		/// Gets the <see cref="FilmDetailsViewModel"/> for the supplied episodeId
		/// </summary>
		/// <param name="episodeId">The ID of the episode to get data for</param>
		/// <returns><see cref="FilmDetailsViewModel"/></returns>
		Task<FilmDetailsViewModel> GetFilmDetailsViewModelAsync(int episodeId);
	}
}
