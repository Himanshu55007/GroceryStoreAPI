using AutoMapper;
using GroceryStoreAPI.Frameworks;

namespace GroceryStoreAPI.Tests.UnitTests.TestHelpers
{
	public class AutoMapperConfigurationBuilder
	{
		public readonly IMapper Mapper;

		public AutoMapperConfigurationBuilder()
		{
			var config = new MapperConfiguration(
			 cfg =>
			 {
				 cfg.AddProfile<AutoMapperConfiguration>();
			 });

			Mapper = config.CreateMapper();
		}
	}
}
