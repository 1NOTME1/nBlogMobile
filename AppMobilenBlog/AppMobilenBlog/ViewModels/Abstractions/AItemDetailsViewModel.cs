using AppMobilenBlog.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.Abstractions
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public abstract class AItemDetailsViewModel<T> : BaseViewModel
    {
        private int itemId;
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        public AItemDetailsViewModel(string title)
            : base()
        {
            Title = title;
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
        }
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItem(value).GetAwaiter().GetResult();
            }
        }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        private async void OnCancel()
            => await Shell.Current.GoToAsync("..");
        private async void OnDelete()
        {
            await DataStore.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnUpdate()
            => await GoToUpdatePage();
        protected abstract Task GoToUpdatePage();
        public abstract Task LoadItem(int id);
    }
}
