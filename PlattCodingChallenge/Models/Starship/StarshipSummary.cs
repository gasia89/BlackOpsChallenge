using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Starship
{
	/// <summary>
	/// Model representing data retrieved from the Starship resource.
	/// </summary>
	public class StarshipSummary
	{
		public string Name { get; set; }
		public string Model { get; set; }
		public string Manufacturer { get; set; }

		[JsonProperty("cost_in_credits")]
		public string CostInCredits { get; set; }
		public string Length { get; set; }

		[JsonProperty("max_atmosphering_speed")]
		public string MaxAtmospheringSpeed { get; set; }
		public string Crew { get; set; }
		public string Passengers { get; set; }

		[JsonProperty("cargo_capacity")]
		public string CargoCapacity { get; set; }
		public string Consumables { get; set; }

		[JsonProperty("hyperdrive_rating")]
		public string HyperdriveRating { get; set; }
		public string MGLT { get; set; }

		[JsonProperty("starship_class")]
		public string StarshipClass { get; set; }
		public List<string> Pilots { get; set; }
		public List<string> Films { get; set; }
		public string Created { get; set; }
		public string Edited { get; set; }
		public string Url { get; set; }

	}
}
