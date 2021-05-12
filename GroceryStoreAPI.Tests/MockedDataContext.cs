using GroceryStoreAPI.DataAccess.Interfaces;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Tests
{
	public class MockedDataContext<T> : IDataContext<T>
	{
		private const string JsonDatabaseFilePath = @"TestDataIntegrationTests\database.json";

		public MockedDataContext()
		{
			
		}

		public async Task<T> GetDataFromStoreAsync()
		{
			using FileStream openStream = File.OpenRead(JsonDatabaseFilePath);
			var data = await JsonSerializer.DeserializeAsync<T>(openStream,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			return data;
		}

		public async Task SaveDataToStoreAsync(T data)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			using var outputStream = File.Create(JsonDatabaseFilePath);
			await JsonSerializer.SerializeAsync<T>(
				outputStream,
				data, options);
		}
	}
}
