using PlattCodingChallenge.Models.Core;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Vehicle
{
	/// <summary>
	/// Vehicle implementation of the paginated response.
	/// </summary>
	public class PaginatedVehicleResponse : PaginatedResponseBase
	{
		public List<VehicleSummary> Results { get; set; }
	}
}
