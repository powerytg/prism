using Newtonsoft.Json.Linq;
using Prism.API.Networking.Events;
using Prism.API.Storage.Events;
using Prism.API.Storage.Factories;
using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage
{
    public partial class StorageCore
    {
        private void OnPhotoStreamReturned(object sender, PhotoStreamEventArgs e)
        {
            FeatureStream stream = e.Stream;
            var newPhotos = new List<Photo>();

            var evt = new StoragePhotoStreamEventArgs();
            evt.Stream = stream;

            try
            {
                JObject rootObject = JObject.Parse(e.Result);
                int page = int.Parse(rootObject["current_page"].ToString());
                int totalItems = int.Parse(rootObject["total_items"].ToString());

                JToken token;
                if (rootObject.TryGetValue("photos", out token))
                {
                    JArray photoArray = JArray.Parse(rootObject["photos"].ToString());
                    foreach (JObject photoObject in photoArray)
                    {
                        Photo photo = PhotoFactory.PhotoWithJObject(photoObject);
                        if (photo != null && !stream.Photos.Contains(photo))
                        {
                            stream.Photos.Add(photo);
                            newPhotos.Add(photo);
                        }
                    }
                }

                // Dispatch event
                evt.Page = page;
                evt.PhotoCount = totalItems;
                evt.NewPhotos.AddRange(newPhotos);

                if (PhotoStreamUpdated != null)
                {
                    PhotoStreamUpdated(this, evt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (PhotoStreamUpdated != null)
                {
                    PhotoStreamUpdated(this, evt);
                }

            }
        }
    }
}
