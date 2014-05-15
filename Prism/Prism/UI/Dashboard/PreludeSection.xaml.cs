using Prism.API.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.Dashboard
{
    public sealed partial class PreludeSection : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PreludeSection()
        {
            this.InitializeComponent();

            // Events
            StorageCore.Instance.CurrentUserInfoUpdated += OnCurrentInfoUpdated;
            PhotoListView.LoadingStarted += OnLoadingStarted;
            PhotoListView.LoadingComplete += OnLoadingComplete;
        }

        private async void OnLoadingStarted(object sender, EventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.ShowAsync();
        }

        private async void OnLoadingComplete(object sender, EventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();
        }

        private void OnCurrentInfoUpdated(object sender, EventArgs e)
        {
            //PhotoListView.Stream = StorageCore.Instance.CurrentUser.PhotoStream;
            PhotoListView.Stream = StorageCore.Instance.PopularStream;
        }

    }
}
