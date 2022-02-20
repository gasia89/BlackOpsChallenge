namespace PlattCodingChallenge.Models.Planet
{
	/// <summary>
	/// Model for displaying planet details.
	/// </summary>
	public class PlanetDetailsViewModel
	{
		public PlanetDetailsViewModel(PlanetSummary summary)
		{
			Name = summary.Name;
			Population = summary.Population;
			Terrain = summary.Terrain;
			LengthOfYear = summary.OrbitalPeriod;
			Diameter = summary.Diameter;
		}

		public string Name { get; set; }

		public string Population { get; set; }

		public string Diameter { get; set; }

		public string Terrain { get; set; }

		public string LengthOfYear { get; set; }

		public string FormattedPopulation => Population == "unknown" ? "unknown" : long.Parse(Population).ToString("N0");
	}
}
