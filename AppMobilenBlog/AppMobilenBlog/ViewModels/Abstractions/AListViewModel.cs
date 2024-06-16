using AppMobilenBlog.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.Abstractions
{
    public abstract class AListViewModel<T> : BaseViewModel where T : class
    {
        #region Fields
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        private T _selectedItem;
        #endregion
        #region Properties
        public ObservableCollection<T> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<T> ItemTapped { get; }
        #endregion
        #region Constructor
        public AListViewModel(string title)
        {
            Title = title;
            Items = new ObservableCollection<T>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<T>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }
        #endregion
        #region Methods
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
            SelectedItem = default(T);
        }
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public abstract Task GoToAddPage();
        public async void OnAddItem(object obj) => await GoToAddPage();
        public abstract Task GoToDetailsPage(T item);

        async void OnItemSelected(T item)
        {
            if (item == null)
                return;
            await GoToDetailsPage(item);
        }
        #endregion
    }
}
