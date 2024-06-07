using AppMobilenBlog.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services.Abstract
{
    public abstract class AListDataStore<T> : IDataStore<T>
    {
        public List<T> items;
        public AListDataStore()
        {
        }
        public async Task<bool> AddItemAsync(T item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }
        public abstract T Find(T item);
        public abstract T Find(int id);
        public async Task<bool> UpdateItemAsync(T item)
        {
            var oldItem = Find(item);
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Find(id);
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }
        public async Task<T> GetItemAsync(int id)
            => await Task.FromResult(Find(id));
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(items);
    }
}
