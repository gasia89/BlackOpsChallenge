using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Planet
{
	/// <summary>
	/// Model for displaying bulk planet data.
	/// </summary>
	public class AllPlanetsViewModel
	{
		public List<PlanetDetailsViewModel> Planets { get; set; } = new List<PlanetDetailsViewModel>();

		public double AverageDiameter { get; set; }
    }
}
