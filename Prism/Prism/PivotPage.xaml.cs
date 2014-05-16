using Prism.API.Networking;
using Prism.API.Storage;
using Prism.API.Storage.Events;
using Prism.Common;
using Prism.UI.Login;
using Prism.UI.ProCam;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Prism
{
    public sealed partial class PivotPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public PivotPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // Events
            APICore.Instance.GetCurrentUserInfoFailed += OnGetCurrentUserInfoFailed;
            StorageCore.Instance.CurrentUserInfoUpdated += OnGetCurrentUserInfoComplete;
            StorageCore.Instance.PhotoStreamUpdated += OnPhotoStreamUpdated;

            // Refresh user info
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.ShowAsync();

            APICore.Instance.GetCurrentUserInfoAsync();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void OnGetCurrentUserInfoComplete(object sender, EventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();
        }

        private async void OnGetCurrentUserInfoFailed(object sender, EventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();

            MessageDialog dialog = new MessageDialog("Cannot verify user info. Please try again");
            dialog.Commands.Add(new UICommand("Retry", OnRetryGetUserInfo));
            await dialog.ShowAsync();
        }

        private async void OnRetryGetUserInfo(IUICommand command)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.ShowAsync();

            APICore.Instance.GetCurrentUserInfoAsync();
        }

        private void OnPhotoStreamUpdated(object sender, StoragePhotoStreamEventArgs e)
        {
            // Only concern about the first user page
            BackgroundView.ImageUrl = e.Stream.Photos[0].MediumImageUrl;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.PickSingleFileAndContinue();
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs fileOpenPickerContinuationEventArgs)
        {
            if (fileOpenPickerContinuationEventArgs.Files != null)
            {
                
            }
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProCamPage));
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            APICore.Instance.ClearAcessCredentials();

            // Go back to login page
            this.Frame.Navigate(typeof(LoginPage));
            if (this.Frame.CanGoBack)
            {
                this.Frame.BackStack.RemoveAt(0);
            }
        }

        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 1)
            {
                CameraBackgroundView.Visibility = Visibility.Visible;

                if (!CameraBackgroundView.Initialized)
                {
                    await CameraBackgroundView.InitializeAsync();
                }
                else
                {
                    await CameraBackgroundView.ResumePreviewAsync();
                }
            }
            else
            {
                CameraBackgroundView.Visibility = Visibility.Collapsed;
                await CameraBackgroundView.PausePreviewAsync();
            }
        }
    }
}
