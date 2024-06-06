using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    [QueryProperty(nameof(PostId), nameof(PostId))]
    public class PostDetailViewModel : BaseViewModel
    {
        public IDataStore<Post> DataStore => DependencyService.Get<IDataStore<Post>>();

        private int postId;
        private string title;
        private string content;

        public int PostId
        {
            get => postId;
            set
            {
                postId = value;
                LoadPostId(value);
            }
        }

        public int UserId { get; set; }
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public DateTime? PublicationDate { get; set; }

        public async void LoadPostId(int postId)
        {
            try
            {
                var post = await DataStore.GetItemAsync(postId);
                PostId = post.PostId;
                UserId = post.UserId.Value;
                Title = post.Title;
                Content = post.Content;
                PublicationDate = post.PublicationDate.HasValue ? post.PublicationDate.Value.LocalDateTime : (DateTime?)null;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Post");
            }
        }
    }
}
