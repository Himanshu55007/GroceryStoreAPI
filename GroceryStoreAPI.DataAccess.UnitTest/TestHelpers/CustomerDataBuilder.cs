using GroceryStoreAPI.DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GroceryStoreAPI.DataAccess.UnitTest.TestHelpers
{
	public class CustomerDataBuilder
	{
		public const string CutomerDataJsonFilePath = @"TestData\database.json";

		public readonly CustomersData CustomersData;

		public readonly Customer Customer;

		public readonly Tuple<int, string> CustomerTupleToUpdate;

		public CustomerDataBuilder()
		{
			CustomersData = GetCustomersData();
			Customer = GetCustomerToAdd();
			CustomerTupleToUpdate = GetCustomerTupleToUpdate();
		}

		private Customer GetCustomerToAdd()
		{
			return new Customer { Id = 5, Name = "TestData" };
		}

		private CustomersData GetCustomersData()
		{
			using JsonReader jsonReader = new JsonTextReader(new StringReader(File.ReadAllText(CutomerDataJsonFilePath)));

			JsonSerializer jsonSerializer = new JsonSerializer();

			var data = jsonSerializer.Deserialize<CustomersData>(jsonReader);

			return data;
		}

		public void AddCustomer()
		{
			CustomersData.Customers.Add(Customer);
		}

		private Tuple<int, string> GetCustomerTupleToUpdate()
		{
			const int id = 1;
			const string name = "TestUpdate";

			return Tuple.Create<int, string>(id, name);
		}
	}
}
