using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.People
{
	/// <summary>
	/// Model representing data retrieved from the People resource.
	/// </summary>
	public class ResidentSummary
	{
		[JsonProperty("birth_year")]
		public string BirthYear { get; set; }

		[JsonProperty("eye_color")]
		public string EyeColor { get; set; }
		public List<string> Films { get; set; }
		public string Gender { get; set; }

		[JsonProperty("hair_color")]
		public string HairColor { get; set; }
		public string Height { get; set; }
		public string Homeworld { get; set; }
		public string Mass { get; set; }
		public string Name { get; set; }

		[JsonProperty("skin_color")]
		public string SkinColor { get; set; }
		public string Created { get; set; }
		public string Edited { get; set; }
		public List<string> Species { get; set; }
		public List<string> Starships { get; set; }
		public string Url { get; set; }
		public List<string> Vehicles { get; set; }
	}
}
