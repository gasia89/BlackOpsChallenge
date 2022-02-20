using PlattCodingChallenge.Models.Film;
using System.Collections.Generic;

namespace PlattCodingChallenge.Models.Financial
{
	/// <summary>
	/// Model for displaying starship finacial data by episode.
	/// </summary>
	public class EpisodeStarshipFinancialDetailsViewModel
	{
		public FilmDetailsViewModel FilmDetailsViewModel { get; set; }
		public int NumberOfStarshipsInEpisodeWithCostAndCargo { get; set; }
		public decimal AverageCostPerCargoPerShip { get; set; }
		public IEnumerable<CostPerCargoUnitViewModel> CostPerCargoViewModels { get; set; }
	}
}
