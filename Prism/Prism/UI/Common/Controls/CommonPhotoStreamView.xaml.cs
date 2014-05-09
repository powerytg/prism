using Prism.API.Storage;
using Prism.API.Storage.Events;
using Prism.API.Storage.Models;
using Prism.UI.Common.Renderers.PhotoRenderers;
using Prism.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.Common.Controls
{
    public sealed partial class CommonPhotoStreamView : StreamListViewBase
    {
        private CommonPhotoGroupFactory factory;
        private ObservableCollection<PhotoGroup> PhotoCollection = new ObservableCollection<PhotoGroup>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonPhotoStreamView()
        {
            this.InitializeComponent();

            PhotoListView.ItemsSource = PhotoCollection;

            // Events
            StorageCore.Instance.PhotoStreamUpdated += OnPhotoStreamUpdated;
        }

        private void OnPhotoStreamUpdated(object sender, StoragePhotoStreamEventArgs e)
        {
            factory = new CommonPhotoGroupFactory();
            factory.StreamContext = Stream.Name;
            factory.UserId = Stream.UserId;
            factory.UserName = Stream.UserName;

            if (e.NewPhotos.Count > 0)
            {
                var groups = factory.GeneratePhotoGroups(e.NewPhotos);
                foreach (var group in groups)
                {
                    PhotoCollection.Add(group);
                }
            }
        }

        protected override void OnStreamChanged()
        {
            if (Stream == null)
            {
                return;
            }

            PhotoCollection.Clear();
            if (Stream.Photos.Count > 0)
            {
                var groups = factory.GeneratePhotoGroups(Stream.Photos);
                foreach (var group in groups)
                {
                    PhotoCollection.Add(group);
                }
            }

        }

    }
}
