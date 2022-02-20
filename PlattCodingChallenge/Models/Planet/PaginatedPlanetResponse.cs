using PlattCodingChallenge.Models.Core;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Planet
{
	/// <summary>
	/// Planet implementation of the paginated response.
	/// </summary>
	public class PaginatedPlanetResponse : PaginatedResponseBase
	{
		public List<PlanetSummary> Results { get; set; }
	}
}
