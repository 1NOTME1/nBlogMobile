using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public interface ILikeService
    {
        Task<bool> IsPostLikedByUser(int postId, int userId);
        Task AddLike(int postId, int userId);
        Task<int> CountLikesForPost(int postId);
        Task RemoveLike(int postId, int userId);
        Task<bool> CheckIfUserLikedPost(int postId, int userId); // Dodana metoda
    }
}
