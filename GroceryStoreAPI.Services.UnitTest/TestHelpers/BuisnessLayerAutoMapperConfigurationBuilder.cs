using AutoMapper;
using GroceryStoreAPI.DataAccess.Frameworks;

namespace GroceryStoreAPI.Services.UnitTest.TestHelpers
{
	public class BuisnessLayerAutoMapperConfigurationBuilder
	{
		public readonly IMapper Mapper;

		public BuisnessLayerAutoMapperConfigurationBuilder()
		{
			var config = new MapperConfiguration(
			 cfg =>
			 {
				 cfg.AddProfile<BuisnessLayerAutoMapperConfiguration>();
			 });

			Mapper = config.CreateMapper();
		}
	}
}
