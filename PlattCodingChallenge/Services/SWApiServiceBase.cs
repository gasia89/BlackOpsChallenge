using Microsoft.Extensions.Logging;
using PlattCodingChallenge.Enums;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace PlattCodingChallenge.Services
{
	/// <summary>
	/// Base class that all services that connect to the <see cref="HttpClientName.StarWarsApiClient"/> inherit from.
	/// </summary>
	public class SWApiServiceBase
	{
		#region Protected Fields
		protected readonly ILogger _logger;
		protected readonly HttpClient _httpClient;
		protected readonly Regex _matchId;
		#endregion

		#region Ctor(s)
		public SWApiServiceBase(ILogger logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient(Enum.GetName(typeof(HttpClientName), HttpClientName.StarWarsApiClient));
			_matchId = new Regex(@$"(?<=({_httpClient.BaseAddress}api\/\w+\/))([0-9]+)", RegexOptions.IgnoreCase);
		}
		#endregion
	}
}
