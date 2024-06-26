﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Services;
using Xamarin.Essentials;
using AppMobilenBlog.Views.PostView;
using AppMobilenBlog.Views;
using AppMobilenBlog.Views.CommentView;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class PostsViewModel : AListViewModel<PostForView>
    {
        private readonly CommentDataStore _commentDataStore;
        private readonly ILikeService _likeService;

        #region Constructor
        public PostsViewModel()
    : base("Browse Posts")
        {
            Debug.WriteLine("Initializing PostsViewModel...");
            _commentDataStore = DependencyService.Get<CommentDataStore>();
            _likeService = DependencyService.Get<ILikeService>();

            if (_commentDataStore == null)
                Debug.WriteLine("CommentDataStore is null!");

            if (_likeService == null)
                Debug.WriteLine("LikeService is null!");

            AddCommentCommand = new Command<PostForView>(async (post) => await ExecuteAddCommentCommand(post));
            LikePostCommand = new Command<PostForView>(async (post) => await ExecuteLikePostCommand(post));
            Task.Run(() => LoadItemsAsync());
        }
        #endregion
        #region Methods
        public Command<PostForView> AddCommentCommand { get; private set; }
        public Command<PostForView> LikePostCommand { get; private set; }

        private async Task ExecuteAddCommentCommand(PostForView post)
        {
            if (post != null)
            {
                await Shell.Current.GoToAsync($"{nameof(NewCommentPage)}?postId={post.PostId}");
            }
        }
        private async Task ExecuteLikePostCommand(PostForView post)
        {
            if (post == null || IsBusy) return;

            IsBusy = true;
            try
            {
                var currentUserId = Preferences.Get("CurrentUserId", 0);
                if (currentUserId == 0)
                {
                    Debug.WriteLine("No user ID available.");
                    throw new Exception("Current user ID is not set.");
                }

                if (_likeService == null)
                {
                    Debug.WriteLine("LikeService is not initialized.");
                    throw new Exception("LikeService instance is null.");
                }

                var alreadyLiked = await _likeService.CheckIfUserLikedPost(post.PostId, currentUserId);
                if (!alreadyLiked)
                {
                    await _likeService.AddLike(post.PostId, currentUserId);
                    post.LikeCount++;
                    OnPropertyChanged(nameof(post.LikeCount));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error liking post: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task LoadItemsAsync()
        {
            IsBusy = true;
            try
            {
                var items = await DataStore?.GetItemsAsync(true);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        if (_commentDataStore != null)
                        {
                            item.CommentCount = await _commentDataStore.GetCommentCountForPostAsync(item.PostId);
                            Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override Task GoToAddPage()
            => Shell.Current.GoToAsync(nameof(NewPostPage));

        public override Task GoToDetailsPage(PostForView post)
            => Shell.Current.GoToAsync($"{nameof(PostDetailPage)}?{nameof(PostDetailViewModel.ItemId)}={post.PostId}");
        #endregion
    }
}
