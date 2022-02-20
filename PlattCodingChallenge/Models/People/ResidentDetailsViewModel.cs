namespace PlattCodingChallenge.Models.People
{
	/// <summary>
	/// Model for displaying resident information.
	/// </summary>
	public class ResidentDetailsViewModel
	{
		public ResidentDetailsViewModel(ResidentSummary summary)
		{
			Name = summary.Name;
			Height = summary.Height;
			Weight = summary.Mass;
			Gender = summary.Gender;
			HairColor = summary.HairColor;
			EyeColor = summary.EyeColor;
			SkinColor = summary.SkinColor;
		}

		public string Name { get; set; }

		public string Height { get; set; }

		public string Weight { get; set; }

		public string Gender { get; set; }

		public string HairColor { get; set; }

		public string EyeColor { get; set; }

		public string SkinColor { get; set; }
    }
}
