using PlattCodingChallenge.Models.People;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with People data.
	/// </summary>
	public interface IPeopleService
	{
		/// <summary>
		/// Assembles a <see cref="PlanetResidentsViewModel"/> for displaying planetary resident data.
		/// </summary>
		/// <param name="planetName">The name of the planet to retrieve resident data for.</param>
		/// <returns><see cref="PlanetResidentsViewModel"/></returns>
		Task<PlanetResidentsViewModel> GetPlanetResidentsViewModelAsync(string planetName);
	}
}
