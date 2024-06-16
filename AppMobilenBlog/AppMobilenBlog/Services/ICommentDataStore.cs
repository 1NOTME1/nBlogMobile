using System.Threading.Tasks;
using AppMobilenBlog.ServiceReference;

namespace AppMobilenBlog.Services
{
    public interface ICommentDataStore
    {
        Task<bool> AddItemAsync(CommentForView item);
        Task<int> GetCommentCountForPostAsync(int postId);
    }
}
