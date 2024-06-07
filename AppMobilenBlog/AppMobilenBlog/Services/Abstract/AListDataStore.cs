using AppMobilenBlog.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services.Abstract
{
    public abstract class AListDataStore<T> : ADataStore, IDataStore<T>
    {
        protected List<T> items;
        public AListDataStore()
            : base()
        {
        }
        public abstract Task Refresh();
        public abstract Task<bool> DeleteItemFromService(T item);
        public abstract Task<bool> UpdateItemInService(T item);
        public abstract Task<bool> AddItemToService(T item);
        public async Task<bool> AddItemAsync(T item)
        {
            await AddItemToService(item);
            await Refresh();
            return await Task.FromResult(true);
        }
        public abstract T Find(T item);
        public abstract T Find(int id);
        public async Task<bool> UpdateItemAsync(T item)
        {
            await UpdateItemInService(item);
            await Refresh();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Find(id);
            await DeleteItemFromService(oldItem);
            await Refresh();
            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(int id)
            => await Task.FromResult(Find(id));

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            await Refresh();
            return await Task.FromResult(items);
        }
    }
}
