Struktura projektu AppMobilenBlog
Folder Helpers
Pliki:
PropertyUtil.cs:

Zawiera metody rozszerzające do kopiowania wartości właściwości z jednego obiektu do drugiego.
ResponseHelper.cs:
Zawiera metodę HandleRequest, która obsługuje asynchroniczne wywołania serwisów i zarządza wyjątkami.

StringToListConverter.cs i StringToTagListConverter.cs:
Zawierają konwertery do konwersji ciągów znaków na listy oraz odwrotnie, używane głównie do przetwarzania danych wejściowych/wyjściowych w aplikacji Xamarin.
Folder Services/Abstract
Pliki:
ADataStore.cs:
Abstrakcyjna klasa bazowa dla klas zarządzających danymi, konfigurująca HttpClient i nBlogService.

AListDataStore.cs:
Abstrakcyjna klasa bazowa rozszerzająca ADataStore, implementująca podstawowe operacje CRUD na liście elementów.

Folder Services
Pliki:
CategoryTagService.cs:
Serwis do zarządzania kategoriami i tagami w postach, umożliwiający ich dodawanie i usuwanie.

CommentDataStore.cs:
Implementacja AListDataStore dla komentarzy, zarządzająca operacjami CRUD na komentarzach.

IDataStore.cs:
Interfejs definiujący metody CRUD oraz metody odświeżania danych.

ILikeService.cs:
Interfejs do zarządzania polubieniami postów.

ILoginService.cs:
Interfejs do zarządzania logowaniem użytkowników.

ItemDataStore.cs, LikeService.cs, LoginService.cs, PostDataStore.cs, UserDataStore.cs:
Implementacje różnych serwisów danych (posty, użytkownicy, polubienia), dziedziczące po AListDataStore i realizujące specyficzne operacje dla danego typu danych.

Folder ViewModels/Abstractions
Pliki:
AItemDetailsViewModel.cs, AItemUpdateViewModel.cs, AListViewModel.cs, ANewItemViewModel.cs:
Abstrakcyjne klasy ViewModeli, które implementują wspólne funkcjonalności, takie jak ładowanie, dodawanie, aktualizowanie i usuwanie elementów oraz obsługę komend.
Folder ViewModels/CommentViewModel
Pliki:
CommentsViewModel.cs:
ViewModel zarządzający listą komentarzy dla danego posta.

NewCommentViewModel.cs:
ViewModel do obsługi dodawania nowych komentarzy.

Folder ViewModels/PostViewModel
Pliki:
NewPostViewModel.cs:

ViewModel do obsługi tworzenia nowych postów.
PostDetailViewModel.cs:

ViewModel do obsługi szczegółów postów, umożliwiający edycję i usuwanie kategorii oraz tagów.
PostUpdateViewModel.cs:

ViewModel do obsługi aktualizacji postów.
PostsViewModel.cs:

ViewModel zarządzający listą postów, umożliwiający dodawanie komentarzy i polubień.
Folder ViewModels/UserViewModel
Pliki:
NewUserViewModel.cs:
ViewModel do obsługi tworzenia nowych użytkowników.

UserDetailsViewModel.cs:
ViewModel do obsługi szczegółów użytkowników.

UserUpdateViewModel.cs:
ViewModel do obsługi aktualizacji danych użytkowników.

UserViewModel.cs:
ViewModel zarządzający listą użytkowników.

Folder ViewModels
Pliki:
AboutViewModel.cs:
ViewModel do obsługi strony informacyjnej aplikacji.

BaseViewModel.cs:
Bazowy ViewModel implementujący INotifyPropertyChanged, obsługujący podstawowe funkcjonalności takie jak zarządzanie stanem IsBusy oraz Title.

LoginViewModel.cs:
ViewModel do obsługi logowania użytkowników.

