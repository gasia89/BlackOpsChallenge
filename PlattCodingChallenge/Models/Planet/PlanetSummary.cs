using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Planet
{
	public class PlanetSummary
	{
		public string Climate { get; set; }
		public string Created { get; set; }
		public string Diameter { get; set; }
		public string Edited { get; set; }
		public List<string> Films { get; set; }
		public string Gravity { get; set; }
		public string Name { get; set; }

		[JsonProperty("orbital_period")]
		public string OrbitalPeriod { get; set; }

		public string Population { get; set; }
		public List<string> Residents { get; set; }
		
		[JsonProperty("rotation_period")]
		public string RotationPeriod { get; set; }

		[JsonProperty("surface_water")]
		public string SurfaceWater { get; set; }
		public string Terrain { get; set; }
		public string Url { get; set; }
	}
}
