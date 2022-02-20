namespace PlattCodingChallenge.Models.Planet
{
	/// <summary>
	/// Model for displaying a single planet's data.
	/// </summary>
	public class SinglePlanetViewModel
	{
		public SinglePlanetViewModel(PlanetSummary summary)
		{
			Name = summary.Name;
			LengthOfDay = summary.RotationPeriod;
			LengthOfYear = summary.OrbitalPeriod;
			Diameter = summary.Diameter;
			Climate = summary.Climate;
			Gravity = summary.Gravity;
			SurfaceWaterPercentage = summary.SurfaceWater;
			Population = summary.Population;
		}

		public string Name { get; set; }

		public string LengthOfDay { get; set; }

		public string LengthOfYear { get; set; }

		public string Diameter { get; set; }

		public string Climate { get; set; }

		public string Gravity { get; set; }

		public string SurfaceWaterPercentage { get; set; }

		public string Population { get; set; } = "0";
	}
}
