using GroceryStoreAPI.DataAccess.Data;
using GroceryStoreAPI.DataAccess.Interfaces;
using GroceryStoreAPI.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Services.Frameworks
{
	public static class ServiceExtention
	{
		public static void AddDataAccessConfiguration(this IServiceCollection services)
		{
			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient(typeof(IDataContext<>), typeof(DataContext<>));
		}
	}
}
