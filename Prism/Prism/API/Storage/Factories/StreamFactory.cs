using Newtonsoft.Json.Linq;
using Prism.API.Storage.Events;
using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Factories
{
    public class StreamFactory
    {
        public static List<Photo> PhotosWithJson(FeatureStream stream, string result)
        {
            var newPhotos = new List<Photo>();
            var evt = new StoragePhotoStreamEventArgs();

            try
            {
                JObject rootObject = JObject.Parse(result);
                int page = int.Parse(rootObject["current_page"].ToString());
                int totalItems = int.Parse(rootObject["total_items"].ToString());

                // Update stream photo count
                stream.PhotoCount = totalItems;

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
                evt.Stream = stream;
                evt.Page = page;
                evt.PhotoCount = totalItems;
                evt.NewPhotos.AddRange(newPhotos);

                if (StorageCore.Instance.PhotoStreamUpdated != null)
                {
                    StorageCore.Instance.PhotoStreamUpdated(StorageCore.Instance, evt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (StorageCore.Instance.PhotoStreamUpdated != null)
                {
                    StorageCore.Instance.PhotoStreamUpdated(StorageCore.Instance, evt);
                }
            }

            return newPhotos;
        }
    }
}
