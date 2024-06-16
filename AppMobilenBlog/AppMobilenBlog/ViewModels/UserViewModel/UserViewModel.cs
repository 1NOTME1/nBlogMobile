using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Views.UserView;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    /// <summary>
    /// Implementation of IDataStore for Users
    /// </summary>
    public class UserViewModel : AListViewModel<UserForView>
    {
        #region Constructor
        public UserViewModel()
            :base("User") { }
        #endregion
        #region Methods
        public override Task GoToAddPage()
        =>  Shell.Current.GoToAsync(nameof(NewUserPage));

        public override Task GoToDetailsPage(UserForView item)
        => Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailsViewModel.ItemId)}={item.UserId}");
        #endregion
    }
}
