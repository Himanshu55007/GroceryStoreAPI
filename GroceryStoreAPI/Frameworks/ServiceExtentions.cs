using AutoMapper;
using GroceryStoreAPI.DataAccess.Frameworks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Frameworks
{
	public static class ServiceExtentions
	{
		public static void AddAutoMapperConfiguration(this IServiceCollection services)
		{
			var config = new MapperConfiguration(
			 cfg =>
			 {
				 cfg.AddProfile<AutoMapperConfiguration>();
				 cfg.AddProfile<BuisnessLayerAutoMapperConfiguration>();
			 });

			services.AddSingleton<IMapper>(config.CreateMapper());
		}
	}
}
