using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using System;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    public class NewPostViewModel : BaseViewModel
    {
        public IDataStore<Post> DataStore => DependencyService.Get<IDataStore<Post>>();

        private string title;
        private string content;

        public NewPostViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            // Walidacja, czy pola nie są puste
            return !String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(content);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // Powrót do poprzedniej strony w nawigacji
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            // Tworzenie nowego posta
            Post newPost = new Post()
            {
                PostId = 0,
                UserId = 0,
                Title = title,
                Content = content,
                PublicationDate = DateTime.Now
            };

            // Dodawanie nowego posta do bazy danych
            await DataStore.AddItemAsync(newPost);

            // Powrót do poprzedniej strony w nawigacji
            await Shell.Current.GoToAsync("..");
        }
    }

}
