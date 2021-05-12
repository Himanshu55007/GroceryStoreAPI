using AutoMapper;
using FluentAssertions;
using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.DataAccess.Interfaces;
using GroceryStoreAPI.Services.Interfaces;
using GroceryStoreAPI.Services.Services;
using GroceryStoreAPI.Services.UnitTest.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.UnitTest
{
	[TestClass]
	public class CustomerBusinessServiceTest
	{
		private readonly Mock<ICustomerRepository> CustomerRepository;
		private readonly ICustomerBusinessService CustomerBusinessService;
		private readonly IMapper Mapper;
		private readonly BuisnessLayerAutoMapperConfigurationBuilder MapperConfigurationBuilder;
		public readonly CustomerDataBuilder CustomerDataBuilder;

		public CustomerBusinessServiceTest()
		{
			CustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			MapperConfigurationBuilder = new BuisnessLayerAutoMapperConfigurationBuilder();
			Mapper = MapperConfigurationBuilder.Mapper;
			CustomerDataBuilder = new CustomerDataBuilder(Mapper);
			CustomerBusinessService = new CustomerBusinessService(CustomerRepository.Object, Mapper);
		}

		[TestMethod]
		public async Task GetCustomersAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.Customers;

			#endregion

			#region "Moq SetUp"

			CustomerRepository.Setup(repo => repo.GetCustomersAsync()).Returns(Task.FromResult<IEnumerable<Customer>>(customersData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customers = await CustomerBusinessService.GetCustomersAsync();

			#endregion "WorkflowTest"

			#region "Asserts"

			CustomerRepository.Verify(repo => repo.GetCustomersAsync(), Times.Once);

			customers.Should().NotBeNullOrEmpty();
			customers.Count().Should().Be(customers.Count());

			#endregion
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync()
		{
			#region "Test Data Setup"

			const int idToLookUp = 1;

			var customerData = CustomerDataBuilder.GetCustomerData(1);

			#endregion

			#region "Moq SetUp"

			CustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(idToLookUp)).Returns(Task.FromResult<Customer>(customerData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customer = await CustomerBusinessService.GetCustomerByIdAsync(idToLookUp);

			#endregion

			#region "Asserts"

			CustomerRepository.Verify(repo => repo.GetCustomerByIdAsync(idToLookUp), Times.Once);

			customer.Should().NotBeNull();

			customer.Id.Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task AddCustomerAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.Customers;

			var customerDtoData = CustomerDataBuilder.CustomerDtoData;

			#endregion

			#region "Moq SetUp"

			CustomerRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
							  .Callback(() => CustomerDataBuilder.AddCustomer())
							  .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"

			await CustomerBusinessService.AddCustomerAsync(customerDtoData);

			#endregion

			#region "Asserts"

			CustomerRepository.Verify(repo => repo.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);

			customersData.Where(x => x.Id == customerDtoData.Id && x.Name == customerDtoData.Name).Count().Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task UpdateCustomerAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.Customers;
			var customerTupleToUpdate = CustomerDataBuilder.CustomerTupleToUpdate;

			#endregion

			#region "Moq SetUp"

			CustomerRepository.Setup(repo => repo.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2))
							  .Callback(() => CustomerDataBuilder.UpdateCustomer(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2))
							  .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"

			await CustomerBusinessService.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2);

			#endregion

			#region "Asserts"

			CustomerRepository.Verify(repo => repo.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2), Times.Once);

			customersData.Where(x => x.Id == customerTupleToUpdate.Item1).FirstOrDefault().Name.Should().Be(customerTupleToUpdate.Item2);

			#endregion
		}
	}
}
