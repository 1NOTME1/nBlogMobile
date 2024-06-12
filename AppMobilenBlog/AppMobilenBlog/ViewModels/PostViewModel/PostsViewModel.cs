using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Views;
using AppMobilenBlog.Views.PostView;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class PostsViewModel : AListViewModel<PostForView>
    {
        public PostsViewModel()
            : base("Browse Posts")
        {
        }

        public override Task GoToAddPage()
            => Shell.Current.GoToAsync(nameof(NewPostPage));

        public override Task GoToDetailsPage(PostForView post)
            => Shell.Current.GoToAsync($"{nameof(PostDetailPage)}?{nameof(PostDetailViewModel.ItemId)}={post.PostId}");

    }
}