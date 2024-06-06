using AppMobilenBlog.Models;
using AppMobilenBlog.Services;
using AppMobilenBlog.Views.UserView;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    /// <summary>
    /// Implementation of IDataStore for Users
    /// </summary>
    public class UserViewModel : BaseViewModel
    {
        public IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();
        private User _selectedItem;
        public ObservableCollection<User> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<User> ItemTapped { get; }

        public UserViewModel()
        {
            Title = "User";
            Items = new ObservableCollection<User>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<User>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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
            SelectedItem = null;
        }

        public User SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem()
            => await Shell.Current.GoToAsync(nameof(NewUserPage));

        async void OnItemSelected(User item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailsViewModel.UserId)}={item.UserId}");
        }
    }
}
