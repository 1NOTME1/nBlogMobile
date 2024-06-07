using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    /// <summary>
    /// Interface for DataStore which is used for connecting model with services, and catching data.
    /// </summary>
    /// <typeparam name="T">Generic parametr for model class.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// This method is used for handling Add operation in model and service.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
