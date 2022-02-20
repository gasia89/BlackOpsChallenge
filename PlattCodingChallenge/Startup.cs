using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlattCodingChallenge.Configuration;
using PlattCodingChallenge.Enums;
using PlattCodingChallenge.Interfaces;
using PlattCodingChallenge.Services;
using System;
using System.Net.Http;

namespace PlattCodingChallenge
{
	public class Startup
	{
		#region Ctor(s)
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		#endregion

		#region Public Properties
		public IConfiguration Configuration { get; }
		#endregion

		/// <summary>
		/// Called by the runtime. Use this method to configure the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The collection of service descriptors for this application.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.Configure<PlanetSettings>(Configuration.GetSection("PlanetSettings"));
			SetupHttpClients(services);

			services.AddSingleton<IPlanetService, PlanetService>();
			services.AddSingleton<IPeopleService, PeopleService>();
			services.AddSingleton<IVehicleService, VehicleService>();
			services.AddSingleton<IStarshipService, StarshipService>();
			services.AddSingleton<IFilmService, FilmService>();
			services.AddSingleton<IFinancialService, FinancialService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		#region Private Methods
		/// <summary>
		/// Load the <see cref="ConnectionSettings"/> section from appSettings.config and sets up
		/// <see cref="HttpClient"/> instances for each <see cref="Connection"/> that has a configured <see cref="HttpClientName"/>.
		/// </summary>
		/// <param name="services"></param>
		/// <exception cref="Exception"></exception>
		private void SetupHttpClients(IServiceCollection services)
		{
			ConnectionSettings connectionSettings = new ConnectionSettings();
			Configuration.GetSection("ConnectionSettings").Bind(connectionSettings);

			foreach (Connection connection in connectionSettings.Connections)
			{
				// make sure we actually loaded a connection
				if (connection != default(Connection) &&
					string.IsNullOrWhiteSpace(connection.ClientName) == false &&
					string.IsNullOrWhiteSpace(connection.BaseUrl) == false)
				{
					// is the connection a valid one?
					if (Enum.IsDefined(typeof(HttpClientName), connection.ClientName) &&
						Uri.TryCreate(connection.BaseUrl, UriKind.RelativeOrAbsolute, out Uri connectionBaseUri))
					{
						// add a new HttpClient to the ServiceCollection for the given client.
						services.AddHttpClient(connection.ClientName, c =>
						{
							c.BaseAddress = connectionBaseUri;
						});
					}
					else
					{
						// unrecoverable. If the api connection information was invalid, fail out.
						throw new Exception("Connection settings are invalid.");
					}
				}
				else
				{
					// unrecoverable. If the api connection information was not supplied, fail out.
					throw new Exception("Required connection settings are missing.");
				}
			}
		}
		#endregion
	}
}
