using AutoMapper;
using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.DataAccess.Interfaces;
using GroceryStoreAPI.Services.BusinessObjects;
using GroceryStoreAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Services
{
	public class CustomerBusinessService : ICustomerBusinessService
	{
		public CustomerBusinessService(ICustomerRepository customerRepository, IMapper mapper)
		{
			CustomerRepository = customerRepository;
			Mapper = mapper;
		}

		public ICustomerRepository CustomerRepository { get; }
		public IMapper Mapper { get; }

		public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
		{
			var customers = await CustomerRepository.GetCustomersAsync();
			var customersDto = Mapper.Map<IEnumerable<CustomerDto>>(customers);
			return customersDto;
		}

		public async Task<CustomerDto> GetCustomerByIdAsync(int id)
		{
			var customer = await CustomerRepository.GetCustomerByIdAsync(id);
			var customerDto = Mapper.Map<CustomerDto>(customer);
			return customerDto;
		}

		public async Task AddCustomerAsync(CustomerDto customerDto)
		{
			var customer = Mapper.Map<Customer>(customerDto);
			await CustomerRepository.AddCustomerAsync(customer);
		}

		public async Task UpdateCustomerAsync(int id, string name)
		{
			await CustomerRepository.UpdateCustomerAsync(id, name);
		}
	}
}
