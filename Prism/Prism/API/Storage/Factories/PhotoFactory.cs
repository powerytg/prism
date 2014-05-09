using Newtonsoft.Json.Linq;
using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Factories
{
    public class PhotoFactory
    {
        public static void UpdatePhotoWithJObject(Photo photo, JObject json)
        {
            // Dispatch event
            JToken token;
            if (json.TryGetValue("name", out token))
            {
                photo.Name = json["name"].ToString();
            }

            if (json.TryGetValue("description", out token))
            {
                photo.Description = json["description"].ToString();
            }

            if (json.TryGetValue("times_viewed", out token))
            {
                photo.ViewCount = int.Parse(json["times_viewed"].ToString());
            }

            if (json.TryGetValue("rating", out token))
            {
                photo.Rating = double.Parse(json["rating"].ToString());
            }

            if (json.TryGetValue("created_at", out token))
            {
                photo.CreatedAt = DateTime.Parse(json["created_at"].ToString());
            }

            if (json.TryGetValue("category", out token))
            {
                photo.Category = int.Parse(json["category"].ToString());
            }

            if (json.TryGetValue("privacy", out token))
            {
                photo.IsPrivate = bool.Parse(json["privacy"].ToString());
            }

            if (json.TryGetValue("width", out token))
            {
                photo.Width = int.Parse(json["width"].ToString());
            }

            if (json.TryGetValue("height", out token))
            {
                photo.Height = int.Parse(json["height"].ToString());
            }

            if (json.TryGetValue("votes_count", out token))
            {
                photo.VoteCount = int.Parse(json["votes_count"].ToString());
            }

            if (json.TryGetValue("favorites_count", out token))
            {
                photo.FavCount = int.Parse(json["favorites_count"].ToString());
            }

            if (json.TryGetValue("comments_count", out token))
            {
                photo.CommentCount = int.Parse(json["comments_count"].ToString());
            }

            if (json.TryGetValue("nsfw", out token))
            {
                photo.NSFW = bool.Parse(json["nsfw"].ToString());
            }

            if (json.TryGetValue("image_url", out token))
            {
                photo.ImageUrl = json["image_url"].ToString();
            }

            if (json.TryGetValue("user", out token))
            {
                string userString = json["user"].ToString();
                User owner = UserFactory.UserWithJson(userString);
                photo.UserId = owner.Id;
            }

            if (json.TryGetValue("voted", out token))
            {
                photo.Voted = bool.Parse(json["voted"].ToString());
            }

            if (json.TryGetValue("liked", out token))
            {
                photo.Liked = bool.Parse(json["liked"].ToString());
            }

            if (json.TryGetValue("disliked", out token))
            {
                photo.Disliked = bool.Parse(json["disliked"].ToString());
            }

            if (json.TryGetValue("favorited", out token))
            {
                photo.Faved = bool.Parse(json["favorited"].ToString());
            }

            if (json.TryGetValue("images", out token))
            {
                JArray imageArray = JArray.Parse(json["images"].ToString());
                foreach (JObject imageObject in imageArray)
                {
                    int size = int.Parse(imageObject["size"].ToString());
                    string sizeUrl = imageObject["url"].ToString();
                    if (size == 1)
                    {
                        photo.TinyImageUrl = sizeUrl;
                    }
                    else if (size == 2)
                    {
                        photo.SmallImageUrl = sizeUrl;
                    }
                    else if (size == 3)
                    {
                        photo.MediumImageUrl = sizeUrl;
                    }
                    else if (size == 4)
                    {
                        photo.LargeImageUrl = sizeUrl;
                    }

                }
            }
        }

        public static Photo PhotoWithJson(string json)
        {
            try
            {
                JObject rootObject = JObject.Parse(json);

                string photoId = rootObject["id"].ToString();
                Photo photo;
                if (StorageCore.Instance.PhotoCache.ContainsKey(photoId))
                {
                    photo = StorageCore.Instance.PhotoCache[photoId];
                }
                else
                {
                    photo = new Photo();
                    photo.Id = photoId;

                    StorageCore.Instance.PhotoCache[photoId] = photo;
                }

                UpdatePhotoWithJObject(photo, rootObject);
                return photo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static Photo PhotoWithJObject(JObject rootObject)
        {
            string photoId = rootObject["id"].ToString();
            Photo photo;
            if (StorageCore.Instance.PhotoCache.ContainsKey(photoId))
            {
                photo = StorageCore.Instance.PhotoCache[photoId];
            }
            else
            {
                photo = new Photo();
                photo.Id = photoId;

                StorageCore.Instance.PhotoCache[photoId] = photo;
            }

            UpdatePhotoWithJObject(photo, rootObject);
            return photo;
        }
    }
}
