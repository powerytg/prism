using Prism.API.Networking.Events;
using Prism.API.Storage.Models;
using Prism.API.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Networking
{
    public partial class APICore
    {
        public async Task<string> GetPhotoStreamAsync(FeatureStream stream, int page = 1, int perPage = 20, List<KeyValuePair<string, string>> additionalParams = null)
        {
            string timestamp = DateTimeUtils.GetTimestamp();
            string nonce = GenerateNonce();

            // Encode the request string
            List<KeyValuePair<string, string>> plist = new List<KeyValuePair<string, string>>();
            plist.Add(new KeyValuePair<string, string>("feature", stream.Name));
            plist.Add(new KeyValuePair<string, string>("oauth_consumer_key", consumerKey));
            plist.Add(new KeyValuePair<string, string>("oauth_nonce", nonce));
            plist.Add(new KeyValuePair<string, string>("oauth_signature_method", "HMAC-SHA1"));
            plist.Add(new KeyValuePair<string, string>("oauth_timestamp", timestamp));
            plist.Add(new KeyValuePair<string, string>("oauth_token", AccessToken));
            plist.Add(new KeyValuePair<string, string>("oauth_version", "1.0"));
            plist.Add(new KeyValuePair<string, string>("page", page.ToString()));
            plist.Add(new KeyValuePair<string, string>("rpp", PerPage.ToString()));

            if (stream.UserId != null)
            {
                plist.Add(new KeyValuePair<string, string>("user_id", stream.UserId));
            }
            else if (stream.UserName != null)
            {
                plist.Add(new KeyValuePair<string, string>("username", stream.UserName));
            }

            if (additionalParams != null)
            {
                foreach (var entry in additionalParams)
                {
                    plist.Add(entry);
                }
            }

            string paramString = GenerateParamString(plist);
            string signature = GenerateSignature("GET", AccessTokenSecret, BaseUrl + "/photos", paramString);

            // Create the http request
            string requestUrl = BaseUrl + "/photos?" + paramString + "&oauth_signature=" + signature;

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage resp = await client.GetAsync(requestUrl);
                resp.EnsureSuccessStatusCode();

                var result = await resp.Content.ReadAsStringAsync();

                // Dispatch event
                if (GetPhotoStreamComplete != null)
                {
                    GetPhotoStreamComplete(this, new PhotoStreamEventArgs(stream, result));
                }

                return result;
            }
            catch (Exception ex)
            {
                GetCurrentUserInfoFailed(this, new PhotoStreamEventArgs(stream));
                Debug.WriteLine(ex);

                return null;
            }
        }

    }
}
