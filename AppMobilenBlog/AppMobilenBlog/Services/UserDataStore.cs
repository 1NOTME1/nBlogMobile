using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class UserDataStore : AListDataStore<UserForView>
    {
        public UserDataStore()
           : base()
           => items = nBlogService.UserAllAsync().GetAwaiter().GetResult().ToList();
        public override UserForView Find(UserForView item)
            => items.Where((UserForView arg) => arg.UserId == item.UserId).FirstOrDefault();
        public override UserForView Find(int id)
            => items.FirstOrDefault(s => s.UserId == id);
        public override async Task Refresh()
            => items = (await nBlogService.UserAllAsync()).ToList();
        public override async Task<bool> DeleteItemFromService(UserForView item)
            => await nBlogService.UserDELETEAsync(item.UserId).HandleRequest();
        public override async Task<bool> UpdateItemInService(UserForView item)
        => await nBlogService.UserPUTAsync(item.UserId, item).HandleRequest();
        public override async Task<bool> AddItemToService(UserForView item)
        => await nBlogService.UserPOSTAsync(item).HandleRequest();

    }
}
