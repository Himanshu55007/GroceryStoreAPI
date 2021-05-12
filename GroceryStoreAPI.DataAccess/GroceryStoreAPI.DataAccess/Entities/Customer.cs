using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroceryStoreAPI.DataAccess.Entities
{
	public class Customer
	{
		[JsonPropertyName("id")]
		[Required]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		[Required]
		public string Name { get; set; }
	}
}
