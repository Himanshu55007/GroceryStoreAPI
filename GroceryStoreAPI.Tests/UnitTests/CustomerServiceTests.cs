using AutoMapper;
using FluentAssertions;
using GroceryStoreAPI.Services;
using GroceryStoreAPI.Services.BusinessObjects;
using GroceryStoreAPI.Services.Interfaces;
using GroceryStoreAPI.Tests.UnitTests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Tests.UnitTests
{
	[TestClass]
	public class CustomerServiceTests
	{
		private readonly Mock<ICustomerBusinessService> CustomerBusinessService;
		private readonly CustomerService CustomerService;
		private readonly IMapper Mapper;
		private readonly AutoMapperConfigurationBuilder MapperConfigurationBuilder;
		private readonly CustomerDataBuilder CustomerDataBuilder;

		public CustomerServiceTests()
		{
			CustomerBusinessService = new Mock<ICustomerBusinessService>(MockBehavior.Strict);
			MapperConfigurationBuilder = new AutoMapperConfigurationBuilder();
			Mapper = MapperConfigurationBuilder.Mapper;
			CustomerDataBuilder = new CustomerDataBuilder(Mapper);
			CustomerService = new CustomerService(CustomerBusinessService.Object, Mapper);
		}

		[TestMethod]
		public async Task GetCustomersAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersDto;

			#endregion

			#region "Moq SetUp"

			CustomerBusinessService.Setup(service => service.GetCustomersAsync()).Returns(Task.FromResult<IEnumerable<CustomerDto>>(customersData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customers = await CustomerService.GetCustomersAsync();

			#endregion "WorkflowTest"

			#region "Asserts"

			CustomerBusinessService.Verify(repo => repo.GetCustomersAsync(), Times.Once);

			customers.Should().NotBeNullOrEmpty();
			customers.Count().Should().Be(customers.Count());

			#endregion
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync()
		{
			#region "Test Data Setup"

			const int idToLookUp = 1;

			var customerDtoData = CustomerDataBuilder.GetCustomerDtoData(1);

			#endregion

			#region "Moq SetUp"

			CustomerBusinessService.Setup(repo => repo.GetCustomerByIdAsync(idToLookUp)).Returns(Task.FromResult<CustomerDto>(customerDtoData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customer = await CustomerService.GetCustomerByIdAsync(idToLookUp);

			#endregion

			#region "Asserts"

			CustomerBusinessService.Verify(repo => repo.GetCustomerByIdAsync(idToLookUp), Times.Once);

			customer.Should().NotBeNull();

			customer.Id.Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task AddCustomerAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersDto;

			var customerData = CustomerDataBuilder.CustomerData;

			#endregion

			#region "Moq SetUp"

			CustomerBusinessService.Setup(repo => repo.AddCustomerAsync(It.IsAny<CustomerDto>()))
							  .Callback(() => CustomerDataBuilder.AddCustomer())
							  .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"

			await CustomerService.AddCustomerAsync(customerData);

			#endregion

			#region "Asserts"

			CustomerBusinessService.Verify(repo => repo.AddCustomerAsync(It.IsAny<CustomerDto>()), Times.Once);

			customersData.Where(x => x.Id == customerData.Id && x.Name == customerData.Name).Count().Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task UpdateCustomerAsync()
		{
			#region "Test Data Setup"

			var customersDtoData = CustomerDataBuilder.CustomersDto;
			var customerTupleToUpdate = CustomerDataBuilder.CustomerTupleToUpdate;

			#endregion

			#region "Moq SetUp"

			CustomerBusinessService.Setup(repo => repo.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2))
							  .Callback(() => CustomerDataBuilder.UpdateCustomerDto(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2))
							  .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"
			
			await CustomerService.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2);

			#endregion

			#region "Asserts"

			CustomerBusinessService.Verify(repo => repo.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2), Times.Once);

			customersDtoData.Where(x => x.Id == customerTupleToUpdate.Item1).FirstOrDefault().Name.Should().Be(customerTupleToUpdate.Item2);

			#endregion
		}
	}
}
