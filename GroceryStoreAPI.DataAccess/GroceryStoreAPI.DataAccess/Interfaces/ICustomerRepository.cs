using GroceryStoreAPI.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.DataAccess.Interfaces
{
	public interface ICustomerRepository
	{
		Task<IEnumerable<Customer>> GetCustomersAsync();
		Task<Customer> GetCustomerByIdAsync(int id);
		Task AddCustomerAsync(Customer customer);
		Task UpdateCustomerAsync(int id, string name);
	}
}
