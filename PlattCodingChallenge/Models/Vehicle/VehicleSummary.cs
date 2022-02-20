using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Vehicle
{
	/// <summary>
	/// Model representing data retrieved from the Vehicles resource.
	/// </summary>
	public class VehicleSummary
	{
		[JsonProperty("cargo_capacity")]
		public string CargoCapacity { get; set; }
		public string Consumables { get; set; }

		[JsonProperty("cost_in_credits")]
		public string CostInCredits { get; set; }
		public string Created { get; set; }
		public string Crew { get; set; }
		public string Edited { get; set; }
		public string Length { get; set; }
		public string Manufacturer { get; set; }

		[JsonProperty("max_atmosphering_speed")]
		public string MaxAtmospheringSpeed { get; set; }
		public string Model { get; set; }
		public string Name { get; set; }
		public string Passengers { get; set; }
		public List<string> Pilots { get; set; }
		public List<string> Films { get; set; }
		public string Url { get; set; }

		[JsonProperty("vehicle_class")]
		public string VehicleClass { get; set; }
	}
}
