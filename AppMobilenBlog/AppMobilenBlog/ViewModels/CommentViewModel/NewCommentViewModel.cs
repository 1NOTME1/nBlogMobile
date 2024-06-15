using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    public class NewCommentViewModel : BaseViewModel
    {
        private string _commentText;
        private readonly CommentDataStore _commentDataStore;
        private readonly int _postId;
        private readonly int _userId;

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }

        public ICommand SubmitCommentCommand { get; }

        public NewCommentViewModel(CommentDataStore commentDataStore, int postId, int userId)
        {
            _commentDataStore = commentDataStore;
            _postId = postId;
            _userId = userId;

            SubmitCommentCommand = new Command(async () => await SubmitComment());
        }

        private async Task SubmitComment()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Comment cannot be empty", "OK");
                return;
            }

            // Pobierz nazwę użytkownika (przykładowo z preferencji aplikacji lub innego źródła)
            var userName = Preferences.Get("UserName", string.Empty);
            var userId = Preferences.Get("UserId", 0); // Pobierz user_id z preferencji
            Console.WriteLine($"Retrieved UserName: {userName}, UserId: {userId}");  // Dodanie logowania
            if (string.IsNullOrWhiteSpace(userName) || userId == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User name and ID must be provided", "OK");
                return;
            }

            var newComment = new CommentForView
            {
                PostId = _postId,
                UserId = userId,
                Content = CommentText,
                CommentDate = DateTime.Now,
                UserName = userName  // Ustawienie nazwy użytkownika
            };

            bool success = await _commentDataStore.AddItemAsync(newComment);

            if (success)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add comment", "OK");
            }
        }
    }
}
