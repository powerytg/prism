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
    public class UserFactory
    {
        public static void UpdateUserWithJObject(User user, JObject json)
        {
            // Dispatch event
            JToken token;
            if (json.TryGetValue("username", out token))
            {
                user.UserName = json["username"].ToString();
            }

            if (json.TryGetValue("firstname", out token))
            {
                user.FirstName = json["firstname"].ToString();
            }

            if (json.TryGetValue("lastname", out token))
            {
                user.LastName = json["lastname"].ToString();
            }

            if (json.TryGetValue("registration_date", out token))
            {
                user.RegistrationDate = DateTime.Parse(json["registration_date"].ToString());
            }

            if (json.TryGetValue("about", out token))
            {
                user.About = json["about"].ToString();
            }

            if (json.TryGetValue("domain", out token))
            {
                user.Domain = json["domain"].ToString();
            }

            if (json.TryGetValue("show_nude", out token))
            {
                user.ShowNude = bool.Parse(json["show_nude"].ToString());
            }

            if (json.TryGetValue("fullname", out token))
            {
                user.FullName = json["fullname"].ToString();
            }

            if (json.TryGetValue("userpic_url", out token))
            {
                user.AvatarUrl = json["userpic_url"].ToString();
            }

            if (json.TryGetValue("upgrade_status", out token))
            {
                user.UpgradeStatus = int.Parse(json["upgrade_status"].ToString());
            }

            if (json.TryGetValue("photos_count", out token))
            {
                user.PhotoCount = int.Parse(json["photos_count"].ToString());
            }

            if (json.TryGetValue("affection", out token))
            {
                user.Affection = int.Parse(json["affection"].ToString());
            }

            if (json.TryGetValue("in_favorites_count", out token))
            {
                user.FavCount = int.Parse(json["in_favorites_count"].ToString());
            }

            if (json.TryGetValue("friends_count", out token))
            {
                user.FriendCount = int.Parse(json["friends_count"].ToString());
            }

            if (json.TryGetValue("followers_count", out token))
            {
                user.FollowerCount = int.Parse(json["followers_count"].ToString());
            }

            if (json.TryGetValue("email", out token))
            {
                user.Email = json["email"].ToString();
            }

            if (json.TryGetValue("upload_limit", out token))
            {
                string uploadString = json["upload_limit"].ToString();

                if (uploadString.Length == 0)
                {
                    user.UploadLimit = User.NoUploadLimit;
                }
                else
                {
                    user.UploadLimit = int.Parse(json["upload_limit"].ToString());
                }
            }

            if (json.TryGetValue("upload_limit_expiry", out token))
            {
                user.UploadLimitExpiration = DateTime.Parse(json["upload_limit_expiry"].ToString());
            }

            if (json.TryGetValue("upgrade_type", out token))
            {
                user.UpgradeType = int.Parse(json["upgrade_type"].ToString());
            }

            if (json.TryGetValue("upgrade_status_expiry", out token))
            {
                user.UpgradeStatusExpiration = DateTime.Parse(json["upgrade_status_expiry"].ToString());
            }

            if (json.TryGetValue("contacts", out token))
            {
                user.ContactInfo.Clear();

                JObject contactObject = (JObject)json["contacts"];
                IList<string> keys = contactObject.Properties().Select(p => p.Name).ToList();
                foreach (var key in keys)
                {
                    user.ContactInfo[key] = contactObject[key].ToString();
                }
            }

            if (json.TryGetValue("equipment", out token))
            {
                JObject equipmentObject = (JObject)json["equipment"];
                if (equipmentObject.TryGetValue("camera", out token))
                {
                    user.Cameras.Clear();
                    string cameraString = equipmentObject["camera"].ToString();
                    JArray cameraArray = JArray.Parse(cameraString);
                    for (int i = 0; i < cameraArray.Count; i++)
                    {
                        user.Cameras.Add(cameraArray[i].ToString());
                    }
                }

                if (equipmentObject.TryGetValue("lens", out token))
                {
                    user.Lenses.Clear();
                    string lensString = equipmentObject["lens"].ToString();
                    JArray lensArray = JArray.Parse(lensString);
                    for (int i = 0; i < lensArray.Count; i++)
                    {
                        user.Lenses.Add(lensArray[i].ToString());
                    }
                }
            }

            if (json.TryGetValue("avatars", out token))
            {
                JObject avatarObject = (JObject)json["avatars"];
                if (avatarObject.TryGetValue("default", out token))
                {
                    user.DefaultAvatarUrl = avatarObject["default"]["http"].ToString();
                }
                
                if (avatarObject.TryGetValue("large", out token))
                {
                    user.LargeAvatarUrl = avatarObject["large"]["http"].ToString();
                }

                if (avatarObject.TryGetValue("small", out token))
                {
                    user.SmallAvatarUrl = avatarObject["small"]["http"].ToString();
                }

                if (avatarObject.TryGetValue("tiny", out token))
                {
                    user.TinyAvatarUrl = avatarObject["tiny"]["http"].ToString();
                }

            }

            // Update userid/username for streams
            user.PhotoStream.UserId = user.Id;

            if (user.UserName != null)
            {
                user.PhotoStream.UserName = user.UserName;
            }

            user.FriendStream.UserId = user.Id;

            if (user.UserName != null)
            {
                user.FriendStream.UserName = user.UserName;
            }

            user.FavStream.UserId = user.Id;

            if (user.UserName != null)
            {
                user.FavStream.UserName = user.UserName;
            }
        }

        public static User UserWithJson(string json)
        {
            try
            {
                JObject rootObject = JObject.Parse(json);
                JToken userToken;
                if (rootObject.TryGetValue("user", out userToken))
                {
                    rootObject = (JObject)rootObject["user"];
                }

                string userId = rootObject["id"].ToString();
                User user;
                if (StorageCore.Instance.UserCache.ContainsKey(userId))
                {
                    user = StorageCore.Instance.UserCache[userId];
                }
                else
                {
                    user = new User();
                    user.Id = userId;

                    StorageCore.Instance.UserCache[userId] = user;
                    StorageCore.Instance.UserList.Add(user);
                }

                UpdateUserWithJObject(user, rootObject);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

    }
}
