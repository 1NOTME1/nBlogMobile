using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Services;
using Xamarin.Essentials;

namespace AppMobilenBlog.ViewModels.CommentViewModel
{
    public class NewCommentViewModel : BaseViewModel
    {
        private readonly ICommentDataStore _commentDataStore;

        public NewCommentViewModel(ICommentDataStore commentDataStore, int postId, int userId)
        {
            _commentDataStore = commentDataStore;
            SubmitCommentCommand = new Command(async () => await SubmitComment());
            PostId = postId;
            UserId = userId;
            UserName = Preferences.Get("UserName", "DefaultUserName");
        }

        public Command SubmitCommentCommand { get; }

        private string commentText;
        public string CommentText
        {
            get => commentText;
            set => SetProperty(ref commentText, value);
        }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        private async Task SubmitComment()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Comment cannot be empty", "OK");
                return;
            }

            if (PostId == 0 || UserId == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid post or user", "OK");
                return;
            }

            var newComment = new CommentForView
            {
                PostId = PostId,
                UserId = UserId,
                UserName = UserName,
                Content = CommentText,
                CommentDate = DateTime.Now
            };

            var success = await _commentDataStore.AddItemAsync(newComment);

                await Application.Current.MainPage.DisplayAlert("Success", "Comment added", "OK");
                CommentText = string.Empty;
        }
    }
}
