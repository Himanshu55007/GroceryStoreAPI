using AutoMapper;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.BusinessObjects;

namespace GroceryStoreAPI.Frameworks
{
	public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration()
		{
			CreateMap<Customer, CustomerDto>();
			CreateMap<CustomerDto, Customer>();
		}
	}
}
