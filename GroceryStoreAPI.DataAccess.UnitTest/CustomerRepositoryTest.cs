using GroceryStoreAPI.DataAccess.Entities;
using GroceryStoreAPI.DataAccess.Interfaces;
using GroceryStoreAPI.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using FluentAssertions;
using GroceryStoreAPI.DataAccess.UnitTest.TestHelpers;

namespace GroceryStoreAPI.DataAccess.UnitTest
{
	[TestClass]
	public class CustomerRepositoryTest
	{
		private readonly Mock<IDataContext<CustomersData>> DataContext;
		private readonly ICustomerRepository CustomerRepository;
		private readonly CustomerDataBuilder CustomerDataBuilder;

		public CustomerRepositoryTest()
		{
			DataContext = new Mock<IDataContext<CustomersData>>(MockBehavior.Strict);
			CustomerRepository = new CustomerRepository(DataContext.Object);
			CustomerDataBuilder = new CustomerDataBuilder();
		}

		[TestMethod]
		public async Task GetCustomersAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersData;

			#endregion

			#region "Moq SetUp"

			DataContext.Setup(dataContext => dataContext.GetDataFromStoreAsync()).Returns(Task.FromResult<CustomersData>(customersData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customers = await CustomerRepository.GetCustomersAsync();

			#endregion "WorkflowTest"

			#region "Asserts"

			DataContext.Verify(dataContext => dataContext.GetDataFromStoreAsync(), Times.Once);

			customers.Should().NotBeNullOrEmpty();
			customers.Count().Should().Be(customersData.Customers.Count);

			#endregion
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersData;
			var idToLookUp = 1;

			#endregion

			#region "Moq SetUp"

			DataContext.Setup(dataContext => dataContext.GetDataFromStoreAsync()).Returns(Task.FromResult<CustomersData>(customersData)).Verifiable();

			#endregion

			#region "WorkflowTest"

			var customer = await CustomerRepository.GetCustomerByIdAsync(idToLookUp);

			#endregion

			#region "Asserts"

			DataContext.Verify(dataContext => dataContext.GetDataFromStoreAsync(), Times.Once);

			customer.Should().NotBeNull();

			customer.Id.Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task AddCustomerAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersData;
			var customerToAdd = CustomerDataBuilder.Customer;

			#endregion

			#region "Moq SetUp"

			DataContext.Setup(dataContext => dataContext.GetDataFromStoreAsync())
						.Returns(Task.FromResult<CustomersData>(customersData)).Verifiable();

			DataContext.Setup(dataContext => dataContext.SaveDataToStoreAsync(It.IsAny<CustomersData>()))
					   .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"

			await CustomerRepository.AddCustomerAsync(customerToAdd);

			#endregion

			#region "Asserts"

			DataContext.Verify(dataContext => dataContext.GetDataFromStoreAsync(), Times.Once);
			DataContext.Verify(dataContext => dataContext.SaveDataToStoreAsync(It.IsAny<CustomersData>()), Times.Once);

			customersData.Customers.Where(x => x.Id == customerToAdd.Id && x.Name == customerToAdd.Name).Count().Should().Be(1);

			#endregion
		}

		[TestMethod]
		public async Task UpdateCustomerAsync()
		{
			#region "Test Data Setup"

			var customersData = CustomerDataBuilder.CustomersData;
			var customerTupleToUpdate = CustomerDataBuilder.CustomerTupleToUpdate;
			#endregion

			#region "Moq SetUp"

			DataContext.Setup(dataContext => dataContext.GetDataFromStoreAsync())
						.Returns(Task.FromResult<CustomersData>(customersData)).Verifiable();

			DataContext.Setup(dataContext => dataContext.SaveDataToStoreAsync(It.IsAny<CustomersData>()))
					   .Returns(Task.CompletedTask).Verifiable();

			#endregion

			#region "WorkflowTest"

			await CustomerRepository.UpdateCustomerAsync(customerTupleToUpdate.Item1, customerTupleToUpdate.Item2);

			#endregion

			#region "Asserts"

			DataContext.Verify(dataContext => dataContext.GetDataFromStoreAsync(), Times.Once);
			DataContext.Verify(dataContext => dataContext.SaveDataToStoreAsync(It.IsAny<CustomersData>()), Times.Once);

			customersData.Customers.Where(x => x.Id == customerTupleToUpdate.Item1).FirstOrDefault().Name.Should().Be(customerTupleToUpdate.Item2);

			#endregion
		}
	}
}
