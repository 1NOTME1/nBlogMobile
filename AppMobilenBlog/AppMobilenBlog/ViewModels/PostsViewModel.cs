using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    public class PostsViewModel : BaseViewModel
    {
        public IDataStore<Post> DataStore => DependencyService.Get<IDataStore<Post>>();

        private Post _selectedPost;

        public ObservableCollection<Post> Posts { get; }
        public Command LoadPostsCommand { get; }
        public Command AddPostCommand { get; }
        public Command<Post> PostTapped { get; }

        public PostsViewModel()
        {
            Title = "Browse";
            Posts = new ObservableCollection<Post>();
            LoadPostsCommand = new Command(async () => await ExecuteLoadPostsCommand());

            PostTapped = new Command<Post>(OnPostSelected);

            AddPostCommand = new Command(OnAddPost);
        }

        async Task ExecuteLoadPostsCommand()
        {
            IsBusy = true;

            try
            {
                Posts.Clear();
                var posts = await DataStore.GetItemsAsync(true);
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPost = null;
        }

        public Post SelectedPost
        {
            get => _selectedPost;
            set
            {
                SetProperty(ref _selectedPost, value);
                OnPostSelected(value);
            }
        }

        private async void OnAddPost(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewPostPage));
        }

        async void OnPostSelected(Post post)
        {
            if (post == null)
                return;

            // This will push the PostDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PostDetailPage)}?{nameof(PostDetailViewModel.PostId)}={post.PostId}");
        }
    }
}