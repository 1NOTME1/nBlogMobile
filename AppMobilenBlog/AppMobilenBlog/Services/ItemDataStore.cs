using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class ItemDataStore : ADataStore, IDataStore<Post>
    {
        readonly List<Post> items;

        public ItemDataStore()
            : base() => items = nBlogService.PostAllAsync().GetAwaiter().GetResult().ToList();

        public async Task<bool> AddItemAsync(Post post)
        {
            items.Add(post);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Post post)
        {
            var oldPost = items.Where((Post arg) => arg.PostId == post.PostId).FirstOrDefault();
            items.Remove(oldPost);
            items.Add(post);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldPost = items.Where((Post arg) => arg.PostId == id).FirstOrDefault();
            items.Remove(oldPost);

            return await Task.FromResult(true);
        }

        public async Task<Post> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.PostId == id));
        }

        public async Task<IEnumerable<Post>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}