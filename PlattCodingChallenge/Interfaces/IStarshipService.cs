using PlattCodingChallenge.Models.Starship;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with Starship data.
	/// </summary>
	public interface IStarshipService
	{
		/// <summary>
		/// Gets a <see cref="StarshipSummary"/> for the given starshipId.
		/// </summary>
		/// <param name="starshipId">The ID of the starship to get a summary of.</param>
		/// <returns><see cref="StarshipSummary"/></returns>
		Task<StarshipSummary> GetStarshipSummaryByIdAsync(int starshipId);
	}
}
