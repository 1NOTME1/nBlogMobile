using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class ItemDataStore : AListDataStore<PostForView>
    {
        public ItemDataStore()
            : base() => items = nBlogService.PostAllAsync().GetAwaiter().GetResult().ToList();

        public override async Task Refresh()
            => items = (await nBlogService.PostAllAsync()).ToList();

        public override async Task<bool> DeleteItemFromService(PostForView item)
            => await nBlogService.PostDELETEAsync(item.PostId).HandleRequest();

        public override async Task<bool> UpdateItemInService(PostForView item)
        => await nBlogService.PostPUTAsync(item.PostId, item).HandleRequest();

        public override async Task<bool> AddItemToService(PostForView item)
        => await nBlogService.PostPOSTAsync(item).HandleRequest();

        public override PostForView Find(PostForView item)
        => items.Where((PostForView arg) => arg.PostId == item.PostId).FirstOrDefault();

        public override PostForView Find(int id)
        => items.FirstOrDefault(s => s.PostId == id);
    }
}