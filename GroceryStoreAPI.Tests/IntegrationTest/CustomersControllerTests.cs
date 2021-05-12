using FluentAssertions;
using GroceryStoreAPI.Tests.IntegrationTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Tests.IntegrationTest
{
	[TestClass]
	public class CustomersControllerTests
	{
		private readonly CustomWebApplicationFactory<Startup> Factory;

		public HttpClient Client { get; }

		public CustomersControllerTests()
		{
			Factory = new CustomWebApplicationFactory<Startup>();
			Client = Factory.CreateClient();
		}

		[TestMethod]
		public async Task GetCustomersAsync_ShouldReturnSuccessResponse()
		{
			#region "API Test"
			var response = await Client.GetAsync("api/Customers");

			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			#endregion

			#region "Asserts"

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			response.Content.Headers.ContentType.ToString().Should().Be(@"application/json; charset=utf-8");

			json.Contains("\"id\":1,\"name\":\"Smith\"").Should().BeTrue();

			#endregion
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync_ShouldReturnSuccessResponse()
		{
			#region "API Test"
			var response = await Client.GetAsync("api/Customers/1");

			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			#endregion

			#region "Asserts"

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			response.Content.Headers.ContentType.ToString().Should().Be(@"application/json; charset=utf-8");

			json.Contains("\"id\":1,\"name\":\"Smith\"").Should().BeTrue();

			#endregion
		}

		[TestMethod]
		public async Task PostAsync_ShouldReturnSuccessResponse()
		{
			#region "Test Data"

			var data = "{\"id\":4,\"name\":\"John\"}";

			HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");

			#endregion "Test Data"

			#region "API Test"
			
			var response = await Client.PostAsync("api/Customers", content);

			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			#endregion

			#region "Asserts"

			response.StatusCode.Should().Be(HttpStatusCode.Created);

			#endregion
		}

		[TestMethod]
		public async Task PutAsync_ShouldReturnSuccessResponse()
		{
			#region "Test Data"

			var data = "\"David\"";

			HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
			
			#endregion "Test Data"

			#region "API Test"

			var response = await Client.PutAsync("api/Customers/3", content);

			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			#endregion

			#region "Asserts"

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			#endregion
		}
	}
}
