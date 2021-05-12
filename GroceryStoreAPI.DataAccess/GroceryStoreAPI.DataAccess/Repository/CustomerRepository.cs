using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.DataAccess.Repository
{
	public class CustomerRepository : ICustomerRepository
    {
		private IDataContext<CustomersData> CustomersDataContext { get; }

		public CustomerRepository(IDataContext<CustomersData> customersDataContext)
		{
			CustomersDataContext = customersDataContext;
		}

		public async Task<IEnumerable<Customer>> GetCustomersAsync()
		{
			var customersData = await CustomersDataContext.GetDataFromStoreAsync();

			return customersData.Customers;
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			var customersData = await CustomersDataContext.GetDataFromStoreAsync();
			
			var customer = customersData.Customers.Where(x => x.Id == id).FirstOrDefault();

			return customer;
		}

		public async Task AddCustomerAsync(Customer customer)
		{
			var customersData = await CustomersDataContext.GetDataFromStoreAsync();

			customersData.Customers.Add(customer);

			await CustomersDataContext.SaveDataToStoreAsync(customersData);
		}

		public async Task UpdateCustomerAsync(int id, string name)
		{
			var customersData = await CustomersDataContext.GetDataFromStoreAsync();

			//Find the Customer using Id
			var customer = customersData.Customers.Where(x => x.Id == id).FirstOrDefault();
			customer.Name = name;

			await CustomersDataContext.SaveDataToStoreAsync(customersData); ;
		}
	}
}
