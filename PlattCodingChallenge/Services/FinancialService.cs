using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models.Financial;
using PlattCodingChallenge.Models.Starship;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Provides functionality for working with financial data.
	/// </summary>
	public class FinancialService : IFinancialService
	{
		#region Fields
		private readonly IStarshipService _starshipService;
		private readonly IFilmService _filmService;
		#endregion

		#region Ctor(s)
		public FinancialService(IStarshipService starshipService, IFilmService filmService)
		{
			_starshipService = starshipService;
			_filmService = filmService;
		}
		#endregion

		#region Public Methods
		public async Task<EpisodeStarshipFinancialDetailsViewModel> GetEpisodeStarshipFinancialDetailsViewModelByEpisodeIdAsync(int episodeId)
		{
			EpisodeStarshipFinancialDetailsViewModel episodeFinancialViewModel = new EpisodeStarshipFinancialDetailsViewModel()
			{
				FilmDetailsViewModel = await _filmService.GetFilmDetailsViewModelAsync(episodeId)
			};

			episodeFinancialViewModel.CostPerCargoViewModels = await GetCostPerCargoUnitViewModelsByStarshipIds(episodeFinancialViewModel.FilmDetailsViewModel.StarshipIds);
			episodeFinancialViewModel.AverageCostPerCargoPerShip = episodeFinancialViewModel.CostPerCargoViewModels.Where(x => x.CostPerUnitOfCargo != 0).Select(x => x.CostPerUnitOfCargo).Average();
			episodeFinancialViewModel.NumberOfStarshipsInEpisodeWithCostAndCargo = episodeFinancialViewModel.CostPerCargoViewModels.Where(x=> x.CostPerUnitOfCargo != 0).Count();

			return episodeFinancialViewModel;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets a <see cref="CostPerCargoUnitViewModel"/> for each supplied starshipId.
		/// </summary>
		/// <param name="starshipIds">The ID's of the starships to get a cost analysis of.</param>
		/// <returns>IEnumerable of <see cref="CostPerCargoUnitViewModel"/></returns>
		private async Task<List<CostPerCargoUnitViewModel>> GetCostPerCargoUnitViewModelsByStarshipIds(IEnumerable<int> starshipIds)
		{
			List<CostPerCargoUnitViewModel> costPerCargoUnitViewModels = new List<CostPerCargoUnitViewModel>();

			if (starshipIds.Count() > 0)
			{
				List<CostPerCargoUnitViewModel> validModels = new List<CostPerCargoUnitViewModel>();
				List<CostPerCargoUnitViewModel> invalidModels = new List<CostPerCargoUnitViewModel>();
				

				foreach (int starshipId in starshipIds)
				{
					StarshipSummary starshipSummary = await _starshipService.GetStarshipSummaryByIdAsync(starshipId);

					// only include ships that have calculatable data
					if (starshipSummary != null && int.TryParse(starshipSummary.CostInCredits, out int cost) && int.TryParse(starshipSummary.CargoCapacity, out int capacity))
					{
						validModels.Add(new CostPerCargoUnitViewModel()
						{
							Name = starshipSummary.Name,
							CargoCapacity = starshipSummary.CargoCapacity,
							CostInCredits = starshipSummary.CostInCredits,
							CostPerUnitOfCargo = (decimal)cost / capacity
						});
					}
					else
					{
						invalidModels.Add(new CostPerCargoUnitViewModel()
						{
							Name = starshipSummary.Name,
							CargoCapacity = starshipSummary.CargoCapacity,
							CostInCredits = starshipSummary.CostInCredits,
							CostPerUnitOfCargo = 0
						});
					}
				}
				costPerCargoUnitViewModels = validModels.OrderByDescending(x => x.CostPerUnitOfCargo).ThenByDescending(x => x.CargoCapacity).ToList();
				costPerCargoUnitViewModels.AddRange(invalidModels);
			}

			return costPerCargoUnitViewModels;
		}
		#endregion
	}
}
