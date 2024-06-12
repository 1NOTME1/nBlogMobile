using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string email, string password);
    }
}
