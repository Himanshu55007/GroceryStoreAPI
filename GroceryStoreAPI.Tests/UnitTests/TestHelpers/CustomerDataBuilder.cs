using AutoMapper;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroceryStoreAPI.Tests.UnitTests.TestHelpers
{
	public class CustomerDataBuilder
	{
		private const string CutomerDataJsonFilePath = @"TestDataUnitTests\database.json";

		public readonly IList<CustomerDto> CustomersDto;

		public readonly Customer CustomerData;

		public readonly Tuple<int, string> CustomerTupleToUpdate;

		public IMapper Mapper { get; }

		public CustomerDataBuilder(IMapper mapper)
		{
			CustomersDto = GetCustomersDtoData();
			CustomerData = GetCustomerData();
			CustomerTupleToUpdate = GetCustomerTupleToUpdate();
			Mapper = mapper;
		}

		private IList<CustomerDto> GetCustomersDtoData()
		{
			using JsonReader jsonReader = new JsonTextReader(new StringReader(File.ReadAllText(CutomerDataJsonFilePath)));

			JsonSerializer jsonSerializer = new JsonSerializer();

			var data = jsonSerializer.Deserialize<List<CustomerDto>>(jsonReader);

			return data;
		}

		private Customer GetCustomerData()
		{
			var customer = new Customer { Id = 5, Name = "TestData" };

			return customer;
		}

		public CustomerDto GetCustomerDtoData(int id)
		{
			var customer = CustomersDto.Where(x => x.Id == id).FirstOrDefault();

			return customer;
		}

		public void AddCustomer()
		{
			var customerDto = Mapper.Map<CustomerDto>(CustomerData);
			CustomersDto.Add(customerDto);
		}

		private Tuple<int, string> GetCustomerTupleToUpdate()
		{
			const int id = 1;
			const string name = "TestUpdate";

			return Tuple.Create<int, string>(id, name);
		}

		public void UpdateCustomerDto(int id, string name)
		{
			CustomersDto.Where(x => x.Id == id).FirstOrDefault().Name = name;
		}
	}
}
