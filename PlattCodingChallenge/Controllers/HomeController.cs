using Microsoft.AspNetCore.Mvc;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Models.Financial;
using PlattCodingChallenge.Models.People;
using PlattCodingChallenge.Models.Planet;
using PlattCodingChallenge.Models.Vehicle;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Controllers
{
	public class HomeController : Controller
	{
		#region Fields
		private readonly IVehicleService _vehicleService;
		private readonly IPlanetService _planetService;
		private readonly IPeopleService _peopleService;
		private readonly IFinancialService _financialService;
		#endregion

		#region Ctor(s)
		public HomeController(IVehicleService vehicleService, IPlanetService planetService, IPeopleService peopleService, IFinancialService financialService)
		{
			_vehicleService = vehicleService;
			_planetService = planetService;
			_peopleService = peopleService;
			_financialService = financialService;
		}
		#endregion

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPlanets()
		{
			AllPlanetsViewModel vm = await _planetService.GetAllPlanetsViewModelAsync();

			if (vm == null)
			{
				return NotFound();
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> GetPlanetById(int planetid)
		{
			SinglePlanetViewModel vm;

			if (planetid <= 0)
			{
				return BadRequest();
			}

			vm = await _planetService.GetSinglePlanetViewModelAsync(planetid);

			if (vm == null)
			{
				return NotFound();
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> GetResidentsOfPlanet(string planetname)
		{
			PlanetResidentsViewModel vm;

			if (string.IsNullOrWhiteSpace(planetname))
			{
				return BadRequest();
			}
			vm = await _peopleService.GetPlanetResidentsViewModelAsync(planetname);

			if (vm == null)
			{
				return NotFound();
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> VehicleSummary()
		{
			VehicleSummaryViewModel vm = await _vehicleService.GetVehicleSummaryViewModelAsync();

			if (vm == null)
			{
				return NotFound();
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> GetStarshipCostPerCargoByEpisodeId(int episodeid)
		{
			EpisodeStarshipFinancialDetailsViewModel vm;

			if (episodeid <= 0)
			{
				return BadRequest();
			}

			vm = await _financialService.GetEpisodeStarshipFinancialDetailsViewModelByEpisodeIdAsync(episodeid);
			
			if (vm == null)
			{
				return NotFound();
			}

			return View(vm);
		}
	}
}
