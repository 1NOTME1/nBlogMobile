using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Services;
using System.Linq;
using AppMobilenBlog.Views.CommentView;
using AppMobilenBlog.Views.PostView;
using AppMobilenBlog.Views;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class PostsViewModel : AListViewModel<PostForView>
    {
        private readonly CommentDataStore _commentDataStore;

        public PostsViewModel()
            : base("Browse Posts")
        {
            _commentDataStore = DependencyService.Get<CommentDataStore>();
            AddCommentCommand = new Command<PostForView>(async (post) => await ExecuteAddCommentCommand(post));
            Task.Run(() => LoadItemsAsync()); // Wywołanie metody ładowania elementów w konstruktorze
        }

        public Command<PostForView> AddCommentCommand { get; private set; }

        private async Task ExecuteAddCommentCommand(PostForView post)
        {
            if (post != null)
            {
                // Zakładając, że NewCommentPage akceptuje postId jako parametr
                await Shell.Current.GoToAsync($"{nameof(NewCommentPage)}?postId={post.PostId}");
            }
        }

        private async Task LoadItemsAsync()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    item.CommentCount = await _commentDataStore.GetCommentCountForPostAsync(item.PostId);
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override Task GoToAddPage()
            => Shell.Current.GoToAsync(nameof(NewPostPage));

        public override Task GoToDetailsPage(PostForView post)
            => Shell.Current.GoToAsync($"{nameof(PostDetailPage)}?{nameof(PostDetailViewModel.ItemId)}={post.PostId}");
    }
}
