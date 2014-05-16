using Newtonsoft.Json.Linq;
using Prism.API.Networking;
using Prism.API.Storage;
using Prism.API.Storage.Events;
using Prism.API.Storage.Factories;
using Prism.API.Storage.Models;
using Prism.UI.Common.Renderers.PhotoRenderers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Prism.UI.Models
{
    public class PhotoGroupCollection : ObservableCollection<PhotoGroup>, ISupportIncrementalLoading
    {
        // Events
        public EventHandler LoadingStarted;
        public EventHandler LoadingComplete;

        private CommonPhotoGroupFactory factory;
        private FeatureStream stream;
        public FeatureStream Stream
        {
            get
            {
                return stream;
            }

            set
            {
                stream = value;
                this.Clear();

                // Create a photo group factory
                factory = new CommonPhotoGroupFactory();
                factory.StreamContext = Stream.Name;
                factory.UserId = Stream.UserId;
                factory.UserName = Stream.UserName;
            }
        }

        public bool HasMoreItems
        {
            get 
            {
                return Stream.Photos.Count < Stream.PhotoCount; 
            }
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            int page = Stream.Photos.Count / APICore.PerPage + 1;

            if (LoadingStarted != null)
            {
                LoadingStarted(this, null);
            }

            return AsyncInfo.Run(async c =>
            {
                var json = await APICore.Instance.GetPhotoStreamAsync(Stream, page, APICore.PerPage, APICore.Instance.DefaultPhotoParameters);
                if (json == null)
                {
                    if (LoadingComplete != null)
                    {
                        LoadingComplete(this, null);
                    }

                    return new LoadMoreItemsResult()
                    {
                        Count = 0
                    };
                }

                var newPhotos = StreamFactory.PhotosWithJson(Stream, json);

                var groups = factory.GeneratePhotoGroups(newPhotos);
                foreach (var group in groups)
                {
                    this.Add(group);
                }

                if (LoadingComplete != null)
                {
                    LoadingComplete(this, null);
                }

                return new LoadMoreItemsResult() {
                    Count = (uint)newPhotos.Count
                };
            });
        }

    }
}
