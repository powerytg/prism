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

            return AsyncInfo.Run(async c =>
            {
                Debug.WriteLine("page=" + page.ToString());

                var json = await APICore.Instance.GetPhotoStreamAsync(Stream, page, APICore.PerPage, APICore.Instance.DefaultPhotoParameters);
                var newPhotos = StreamFactory.PhotosWithJson(Stream, json);

                Debug.WriteLine("photos=" + Stream.Photos.Count.ToString());

                var groups = factory.GeneratePhotoGroups(newPhotos);
                foreach (var group in groups)
                {
                    this.Add(group);
                }

                return new LoadMoreItemsResult() {
                    Count = (uint)newPhotos.Count
                };
            });
        }

        

    }
}
