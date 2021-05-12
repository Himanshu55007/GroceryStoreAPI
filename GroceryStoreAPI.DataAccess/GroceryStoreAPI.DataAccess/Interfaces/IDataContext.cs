using System.Threading.Tasks;

namespace GroceryStoreAPI.DataAccess.Interfaces
{
	public interface IDataContext<T>
	{
		Task<T> GetDataFromStoreAsync();

		Task SaveDataToStoreAsync(T data);
	}
}