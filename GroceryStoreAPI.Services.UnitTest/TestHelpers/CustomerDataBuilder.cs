using AutoMapper;
using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.Services.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroceryStoreAPI.Services.UnitTest.TestHelpers
{
	public class CustomerDataBuilder
	{
		private const string CutomerDataJsonFilePath = @"TestData\database.json";

		public readonly IList<Customer> Customers;

		public readonly CustomerDto CustomerDtoData;

		public readonly Tuple<int, string> CustomerTupleToUpdate;

		public IMapper Mapper { get; }

		public CustomerDataBuilder(IMapper mapper)
		{
			Customers = GetCustomersData();
			CustomerDtoData = GetCustomerDtoData();
			CustomerTupleToUpdate = GetCustomerTupleToUpdate();
			Mapper = mapper;
		}

		private IList<Customer> GetCustomersData()
		{
			using JsonReader jsonReader = new JsonTextReader(new StringReader(File.ReadAllText(CutomerDataJsonFilePath)));

			JsonSerializer jsonSerializer = new JsonSerializer();

			var data = jsonSerializer.Deserialize<List<Customer>>(jsonReader);

			return data;
		}

		private CustomerDto GetCustomerDtoData()
		{
			var customerDto = new CustomerDto { Id = 5, Name = "TestData" };

			return customerDto;
		}

		public Customer GetCustomerData(int id)
		{
			var customer = Customers.Where(x => x.Id == id).FirstOrDefault();

			return customer;
		}

		public void AddCustomer()
		{
			var customer = Mapper.Map<Customer>(CustomerDtoData);

			Customers.Add(customer);
		}

		private Tuple<int, string> GetCustomerTupleToUpdate()
		{
			const int id = 1;
			const string name = "TestUpdate";

			return Tuple.Create<int, string>(id, name);
		}

		public void UpdateCustomer(int id, string name)
		{
			Customers.Where(x => x.Id == id).FirstOrDefault().Name = name;
		}
	}
}
