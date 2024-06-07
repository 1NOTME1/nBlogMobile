using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class ItemDataStore : AListDataStore<Post>
    {
        public ItemDataStore()
            : base() => items = nBlogService.PostAllAsync().GetAwaiter().GetResult().ToList();

        public override async Task Refresh()
            => items = (await nBlogService.PostAllAsync()).ToList();

        public override async Task<bool> DeleteItemFromService(Post item)
            => await nBlogService.PostDELETEAsync(item.PostId).HandleRequest();

        public override async Task<bool> UpdateItemInService(Post item)
        => await nBlogService.PostPUTAsync(item.PostId, item).HandleRequest();

        public override async Task<bool> AddItemToService(Post item)
        => await nBlogService.PostPOSTAsync(item).HandleRequest();

        public override Post Find(Post item)
        => items.Where((Post arg) => arg.PostId == item.PostId).FirstOrDefault();

        public override Post Find(int id)
        => items.FirstOrDefault(s => s.PostId == id);
    }
}