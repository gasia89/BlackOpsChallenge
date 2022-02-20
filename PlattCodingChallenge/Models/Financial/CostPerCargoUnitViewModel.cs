namespace PlattCodingChallenge.Models.Financial
{
	/// <summary>
	/// Model for displaying cost analysis data for starship cargo capacity vs ship cost.
	/// </summary>
	public class CostPerCargoUnitViewModel
	{
		public string Name { get; set; }
		public string CargoCapacity { get; set; }
		public string CostInCredits { get; set; }
		public decimal CostPerUnitOfCargo { get; set; }
	}
}
