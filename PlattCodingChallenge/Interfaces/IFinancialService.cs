using PlattCodingChallenge.Models.Financial;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with Financial data.
	/// </summary>
	public interface IFinancialService
	{
		/// <summary>
		/// Gets a <see cref="EpisodeStarshipFinancialDetailsViewModel"/> for the supplied episodeId.
		/// </summary>
		/// <param name="episodeId">The ID of the episode to get a cost analysis for.</param>
		/// <returns><see cref="EpisodeStarshipFinancialDetailsViewModel"/></returns>
		Task<EpisodeStarshipFinancialDetailsViewModel> GetEpisodeStarshipFinancialDetailsViewModelByEpisodeIdAsync(int episodeId);
	}
}
