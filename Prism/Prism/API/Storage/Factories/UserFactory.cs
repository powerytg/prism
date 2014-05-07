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
