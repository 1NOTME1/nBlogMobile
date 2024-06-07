using AppMobilenBlog.Services;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.Abstractions
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public abstract class AItemUpdateViewModel<T> : BaseViewModel
    {
        private int itemId;
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        public AItemUpdateViewModel(string title)
        {
            Title = title;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }
        public abstract bool ValidateSave();
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
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private async void OnCancel()
            => await Shell.Current.GoToAsync("..");
        public abstract T SetItem();
        public abstract Task LoadItem(int id);
        private async void OnSave()
        {
            Debug.WriteLine("OnSave method called."); // Dodaj debugowanie

            try
            {
                if (ValidateSave())
                {
                    Debug.WriteLine("Saving user data..."); // Dodaj debugowanie
                    await DataStore.UpdateItemAsync(SetItem());
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Debug.WriteLine("Validation failed."); // Dodaj debugowanie
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during save operation: {ex.Message}");
            }
        }


    }
}
