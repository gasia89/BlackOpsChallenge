using PlattCodingChallenge.Models;
using PlattCodingChallenge.Models.Vehicle;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Interfaces
{
	/// <summary>
	/// Interface for working with Vehicle data.
	/// </summary>
	public interface IVehicleService
	{
		/// <summary>
		/// Assembles a <see cref="VehicleSummaryViewModel"/> for displaying vehicle data.
		/// </summary>
		/// <returns><see cref="VehicleSummaryViewModel"/></returns>
		Task<VehicleSummaryViewModel> GetVehicleSummaryViewModelAsync();
	}
}
