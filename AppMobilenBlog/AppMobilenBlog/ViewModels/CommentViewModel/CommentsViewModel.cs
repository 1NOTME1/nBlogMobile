using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppMobilenBlog.ViewModels.CommentViewModel
{
    public class CommentsViewModel : BaseViewModel
    {
        public ObservableCollection<CommentForView> Comments { get; private set; } = new ObservableCollection<CommentForView>();
        private CommentDataStore commentDataStore = new CommentDataStore();
        private int _postId;

        public ICommand LoadCommentsCommand { get; }
        public ICommand AddCommentCommand { get; }
        public ICommand DeleteCommentCommand { get; }

        public CommentsViewModel(int postId)
        {
            _postId = postId;

            LoadCommentsCommand = new Command(async () => await LoadComments());
            AddCommentCommand = new Command<string>(async (commentText) => await AddComment(commentText));
            DeleteCommentCommand = new Command<int>(async (commentId) => await DeleteComment(commentId));
        }

        private async Task LoadComments()
        {
            IsBusy = true;
            var comments = await commentDataStore.GetItemsAsync();
            Comments.Clear();
            foreach (var comment in comments.Where(c => c.PostId == _postId))
            {
                Comments.Add(comment);
            }
            IsBusy = false;
        }

        private async Task AddComment(string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Comment cannot be empty", "OK");
                return;
            }

            var userId = Preferences.Get("UserId", 0); // Pobierz UserId z preferencji

            var newComment = new CommentForView
            {
                PostId = _postId,
                UserId = userId,
                Content = commentText,
                CommentDate = DateTime.Now
            };

            if (await commentDataStore.AddItemAsync(newComment))
            {
                Comments.Add(newComment);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add comment", "OK");
            }
        }

        private async Task DeleteComment(int commentId)
        {
            if (await commentDataStore.DeleteItemAsync(commentId))
            {
                var comment = Comments.FirstOrDefault(c => c.CommentId == commentId);
                if (comment != null)
                {
                    Comments.Remove(comment);
                }
            }
        }
    }
}
