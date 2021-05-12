using GroceryStoreAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Tests.IntegrationTest.Helpers
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureTestServices(services =>
			{
				builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
				services.AddTransient(typeof(IDataContext<>), typeof(MockedDataContext<>));
			});
		}
	}
}
