using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class CommentDataStore : AListDataStore<CommentForView>, ICommentDataStore
    {
        public CommentDataStore() : base()
        {
            LoadInitialData().ConfigureAwait(false);
        }

        private async Task<IEnumerable<CommentForView>> LoadInitialData()
        {
            items = (await nBlogService.CommentAllAsync()).ToList();
            return items;
        }

        public override Task Refresh() => LoadInitialData();

        public override async Task<bool> AddItemToService(CommentForView item)
        {
            try
            {
                var response = await nBlogService.CommentPOSTAsync(item);
                if (response.CommentId > 0)
                {
                    item.CommentId = response.CommentId;
                    items.Add(item);
                    return true;
                }
            }
            catch (AppMobilenBlog.ServiceReference.ApiException ex)
            {
                Debug.WriteLine($"Error adding comment: {ex.Message}");
                Debug.WriteLine($"HTTP Status: {ex.StatusCode}");
                Debug.WriteLine($"Response: {ex.Response}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return false;
        }

        public async Task<int> GetCommentCountForPostAsync(int postId)
        {
            var comments = await nBlogService.CommentAllAsync();
            return comments.Count(c => c.PostId == postId);
        }

        public override async Task<bool> UpdateItemInService(CommentForView item) => await nBlogService.CommentPUTAsync(item.CommentId, item).HandleRequest();

        public override async Task<bool> DeleteItemFromService(CommentForView item) => await nBlogService.CommentDELETEAsync(item.CommentId).HandleRequest();

        public override CommentForView Find(CommentForView item) => items.FirstOrDefault(x => x.CommentId == item.CommentId);

        public override CommentForView Find(int id) => items.FirstOrDefault(x => x.CommentId == id);

        public Task<bool> AddItemAsync(CommentForView item) => AddItemToService(item);
    }
}
