using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStoreAPI.Services.BusinessObjects;

namespace GroceryStoreAPI.Services
{
	public class CustomerService
	{
		public ICustomerBusinessService CustomerBusinessService { get; }
		public IMapper Mapper { get; }

		public CustomerService(ICustomerBusinessService customerBusinessService, IMapper mapper)
		{
			CustomerBusinessService = customerBusinessService;
			Mapper = mapper;
		}

		public async Task<IEnumerable<Customer>> GetCustomersAsync()
		{
			var customersDto = await CustomerBusinessService.GetCustomersAsync();

			var customers = Mapper.Map<IEnumerable<Customer>>(customersDto);

			return customers;
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			var customerDto = await CustomerBusinessService.GetCustomerByIdAsync(id);

			var customer = Mapper.Map<Customer>(customerDto);

			return customer;
		}

		public async Task AddCustomerAsync(Customer customer)
		{
			var customerDto = Mapper.Map<CustomerDto>(customer);

			await CustomerBusinessService.AddCustomerAsync(customerDto);
		}

		public async Task UpdateCustomerAsync(int id, string name)
		{
			await CustomerBusinessService.UpdateCustomerAsync(id, name);
		}
	}
}
