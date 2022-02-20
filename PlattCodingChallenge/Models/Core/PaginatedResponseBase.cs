using System.Text.RegularExpressions;

namespace PlattCodingChallenge.Models.Core
{
	/// <summary>
	/// Base class for deserializing paginated endpoints. All paginated responses inherit from this class.
	/// </summary>
	public class PaginatedResponseBase
	{
		public int Count { get; set; }
		public string Next { get; set; }
		public string Previous { get; set; }

		public int GetNextPageId()
		{
			Regex matchPageId = new Regex(@"(?<=((http|https):\/\/swapi.dev\/api\/\w+\/)(\?page=)?)([0-9]+)", RegexOptions.IgnoreCase);
			string matchedId = matchPageId.Match(Next).Value;
			return int.Parse(matchedId);
		}
	}
}
