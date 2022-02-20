using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Vehicle
{
	/// <summary>
	/// Model for displaying vehicle data.
	/// </summary>
	public class VehicleSummaryViewModel
	{
		public int VehicleCount { get; set; }

		public int ManufacturerCount { get; set; }

		public List<VehicleStatsViewModel> Details { get; set; } = new List<VehicleStatsViewModel>();
    }
}
