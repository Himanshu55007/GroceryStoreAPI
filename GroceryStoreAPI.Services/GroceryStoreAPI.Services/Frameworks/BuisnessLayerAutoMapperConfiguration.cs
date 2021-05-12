using AutoMapper;
using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.Services.BusinessObjects;

namespace GroceryStoreAPI.DataAccess.Frameworks
{
	public class BuisnessLayerAutoMapperConfiguration : Profile
	{
		public BuisnessLayerAutoMapperConfiguration()
		{
			CreateMap<Customer, CustomerDto>();
			CreateMap<CustomerDto, Customer>();
		}
	}
}
