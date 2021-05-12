using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GroceryStoreAPI.DataAccess.Entities
{
	public class CustomersData
	{
		[JsonPropertyName("customers")]
		public List<Customer> Customers { get; set; }
	}
}
