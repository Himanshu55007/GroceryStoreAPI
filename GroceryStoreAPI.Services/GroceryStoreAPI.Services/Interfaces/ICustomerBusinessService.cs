using GroceryStoreAPI.Services.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Interfaces
{
	public interface ICustomerBusinessService
	{
		Task<IEnumerable<CustomerDto>> GetCustomersAsync();
		Task<CustomerDto> GetCustomerByIdAsync(int id);
		Task AddCustomerAsync(CustomerDto customer);
		Task UpdateCustomerAsync(int id, string name);
	}
}
