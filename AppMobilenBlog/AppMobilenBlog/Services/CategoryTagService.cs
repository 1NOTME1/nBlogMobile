using AppMobilenBlog.ServiceReference;
using System.Linq;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class CategoryTagService
    {
        private readonly IDataStore<PostForView> _dataStore;

        public CategoryTagService(IDataStore<PostForView> dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task RemoveCategoryAsync(int postId, string categoryName)
        {
            var post = await _dataStore.GetItemAsync(postId);
            if (post != null)
            {
                var categories = post.CategoryData.Split(',').Select(c => c.Trim()).ToList();
                if (categories.Remove(categoryName))
                {
                    post.CategoryData = string.Join(", ", categories);
                    await _dataStore.UpdateItemAsync(post);
                }
            }
        }

        public async Task RemoveTagAsync(int postId, string tagName)
        {
            var post = await _dataStore.GetItemAsync(postId);
            if (post != null)
            {
                var tags = post.TagData.Split(' ').Select(t => t.TrimStart('#')).ToList();
                if (tags.Remove(tagName))
                {
                    post.TagData = string.Join(" ", tags.Select(t => "#" + t));
                    await _dataStore.UpdateItemAsync(post);
                }
            }
        }
    }



}
