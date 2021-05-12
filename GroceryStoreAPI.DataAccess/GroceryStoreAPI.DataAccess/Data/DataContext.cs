using GroceryStoreAPI.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GroceryStoreAPI.DataAccess.Data
{
	public class DataContext<T> : IDataContext<T>
	{
		private const string JsonDatabaseFileKey = "DataBaseFilePath";

		public DataContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public async Task<T> GetDataFromStoreAsync()
		{
			using FileStream openStream = File.OpenRead(Configuration.GetSection(JsonDatabaseFileKey).Value);
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

			using var outputStream = File.Create(Configuration.GetSection(JsonDatabaseFileKey).Value);
			await JsonSerializer.SerializeAsync<T>(
				outputStream,
				data, options);
		}
	}
}
