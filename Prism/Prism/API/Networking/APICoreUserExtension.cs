using Prism.API.Networking.Events;
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
        public async void GetCurrentUserInfoAsync()
        {
            string timestamp = DateTimeUtils.GetTimestamp();
            string nonce = Guid.NewGuid().ToString().Replace("-", null);

            // Encode the request string
            string paramString = "oauth_consumer_key=" + consumerKey;
            paramString += "&oauth_nonce=" + nonce;
            paramString += "&oauth_signature_method=HMAC-SHA1";
            paramString += "&oauth_timestamp=" + timestamp;
            paramString += "&oauth_token=" + AccessToken;
            paramString += "&oauth_version=1.0";

            string signature = GenerateSignature("GET", AccessTokenSecret, "https://api.500px.com/v1/users", paramString);

            // Create the http request
            string requestUrl = "https://api.500px.com/v1/users?" + paramString + "&oauth_signature=" + signature;

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage resp = await client.GetAsync(requestUrl);
                resp.EnsureSuccessStatusCode();

                var result = await resp.Content.ReadAsStringAsync();
                GetCurrentUserInfoComplete(this, new APIEventArgs(result));

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                GetCurrentUserInfoFailed(this, null);
            }
        }

    }
}
