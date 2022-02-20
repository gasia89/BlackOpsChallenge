using PlattCodingChallenge.Models;
using PlattCodingChallenge.Models.Planet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with Planet data.
	/// </summary>
	public interface IPlanetService
	{
		/// <summary>
		/// Assembles a <see cref="AllPlanetsViewModel"/> for displaying Planetary data.
		/// </summary>
		/// <returns><see cref="AllPlanetsViewModel"/></returns>
		Task<AllPlanetsViewModel> GetAllPlanetsViewModelAsync();

		/// <summary>
		/// Assembles a <see cref="SinglePlanetViewModel"/> for displaying Planetary data.
		/// </summary>
		/// <param name="planetId">The ID of the planet to retrieve data for.</param>
		/// <returns><see cref="SinglePlanetViewModel"/></returns>
		Task<SinglePlanetViewModel> GetSinglePlanetViewModelAsync(int planetId);

		/// <summary>
		/// Gets a <see cref="PlanetSummary"/> for the supplied planetName.
		/// </summary>
		/// <param name="planetName">The name of the planet to get data for.</param>
		/// <returns><see cref="PlanetSummary"/></returns>
		Task<PlanetSummary> GetPlanetSummaryByNameAsync(string planetName);

		/// <summary>
		/// Attempts to retreive a list of <see cref="PlanetSummary"/> from the data source
		/// </summary>
		/// <returns><see cref="PlanetSummary"/></returns>
		Task<List<PlanetSummary>> GetPlanetSummariesAsync();
	}
}
