using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace AppMobilenBlog.Services
{
    public class LikeService : AListDataStore<LikeForView>, ILikeService
    {
        public LikeService()
            : base()
        {
            items = nBlogService.LikeAllAsync().GetAwaiter().GetResult().ToList();
        }

        public override LikeForView Find(LikeForView item)
        {
            return items.Where((LikeForView arg) => arg.LikeId == item.LikeId).FirstOrDefault();
        }

        public override LikeForView Find(int id)
        {
            return items.FirstOrDefault(l => l.LikeId == id);
        }

        public override async Task Refresh()
        {
            items = (await nBlogService.LikeAllAsync()).ToList();
        }

        public override async Task<bool> DeleteItemFromService(LikeForView item)
        {
            return await nBlogService.LikeDELETEAsync(item.LikeId).HandleRequest();
        }

        public override async Task<bool> UpdateItemInService(LikeForView item)
        {
            return await nBlogService.LikePUTAsync(item.LikeId, item).HandleRequest();
        }

        public override async Task<bool> AddItemToService(LikeForView item)
        {
            return await nBlogService.LikePOSTAsync(item).HandleRequest();
        }

        public async Task AddLike(int postId, int userId)
        {
            try
            {
                if (nBlogService == null)
                {
                    Debug.WriteLine("Service is not initialized.");
                    return;
                }

                Debug.WriteLine($"Checking if user with ID {userId} exists...");

                var user = await nBlogService.UserGETAsync(userId);
                if (user == null)
                {
                    Debug.WriteLine($"User with ID {userId} does not exist.");
                    return;
                }

                var newLike = new LikeForView { PostId = postId, UserId = userId };
                var response = await nBlogService.LikePOSTAsync(newLike);
                if (response != null && response.LikeId > 0)
                {
                    newLike.LikeId = response.LikeId;
                    items.Add(newLike);
                }
            }
            catch (AppMobilenBlog.ServiceReference.ApiException ex)
            {
                Debug.WriteLine($"API Exception: {ex.Message}, Status Code: {ex.StatusCode}");
                if (ex.StatusCode == 404)
                {
                    Debug.WriteLine("User not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        public async Task<bool> IsPostLikedByUser(int postId, int userId)
        {
            return await Task.FromResult(items.Any(l => l.PostId == postId && l.UserId == userId));
        }

        public async Task<int> CountLikesForPost(int postId)
        {
            return await Task.FromResult(items.Count(l => l.PostId == postId));
        }

        public async Task RemoveLike(int postId, int userId)
        {
            var likeToRemove = items.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
            if (likeToRemove != null)
            {
                items.Remove(likeToRemove);
                await nBlogService.LikeDELETEAsync(likeToRemove.LikeId).HandleRequest();
            }
        }

        public async Task<bool> CheckIfUserLikedPost(int postId, int userId)
        {
            return await Task.FromResult(items.Any(l => l.PostId == postId && l.UserId == userId));
        }
    }
}
