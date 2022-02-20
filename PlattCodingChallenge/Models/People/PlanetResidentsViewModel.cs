using System.Collections.Generic;

namespace PlattCodingChallenge.Models.People
{
	/// <summary>
	/// Model for displaying planetary resident information.
	/// </summary>
	public class PlanetResidentsViewModel
	{
		public List<ResidentDetailsViewModel> Residents { get; set; } = new List<ResidentDetailsViewModel>();
    }
}
