using GroceryStoreAPI.Services.Interfaces;
using GroceryStoreAPI.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Services.Frameworks
{
	public static class ServiceExtention
	{
		public static void AddBuisnessConfiguration(this IServiceCollection services)
		{
			services.AddTransient<ICustomerBusinessService, CustomerBusinessService>();
			services.AddDataAccessConfiguration();
		}
	}
}
