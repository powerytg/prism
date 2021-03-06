﻿using Prism.API.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace Prism.API.Networking
{
    public partial class APICore
    {
        // OAuth2 client id. This is actually the iOS app id
        private string consumerKey = "uNCkCtDof5jj4kI7aIHEdKQ3a2ITLFuVMEeIRdca";

        // OAuth2 client secret
        private string consumerSecret = "pGcF6Kw6q9tqfoSZpkQUIpP0URMPMcVsGqBJe4K6";

        // Callback Url
        public string CallbackUrl = "http://primewp.com/auth";

        // OAuth token
        public string RequestToken { get; set; }

        // OAuth token secret
        public string RequestTokenSecret { get; set; }

        // OAuth token verifier
        public string RequestTokenVerifier { get; set; }

        // OAuth access token
        public string AccessToken { get; set; }

        // OAuth access token secret
        public string AccessTokenSecret { get; set; }

        private string GenerateNonce()
        {
            return Guid.NewGuid().ToString().Replace("-", null);
        }

        private string GenerateParamString(Dictionary<string, string> parameters)
        {
            string paramString = null;
            if (parameters != null)
            {
                var sortedParams = from key in parameters.Keys
                                   orderby key ascending
                                   select key;

                List<string> paramList = new List<string>();
                foreach (string key in sortedParams)
                {
                    string part = key + "=" + parameters[key];
                    paramList.Add(part);
                }

                paramString = string.Join("&", paramList);
                return paramString;
            }
            else
            {
                return null;
            }
        }

        private string GenerateParamString(List<KeyValuePair<string, string>> parameters)
        {
            parameters.Sort(KeyValuePairCompare);
            if (parameters != null)
            {
                parameters.Sort(KeyValuePairCompare);

                List<string> paramList = new List<string>();
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    string part = pair.Key + "=" + pair.Value;
                    paramList.Add(part);
                }

                var paramString = string.Join("&", paramList);
                return paramString;
            }
            else
            {
                return null;
            }
        }

        static int KeyValuePairCompare(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        private string Sha1Encrypt(string baseString, string keyString)
        {
            var crypt = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var buffer = CryptographicBuffer.ConvertStringToBinary(baseString, BinaryStringEncoding.Utf8);
            var keyBuffer = CryptographicBuffer.ConvertStringToBinary(keyString, BinaryStringEncoding.Utf8);
            var key = crypt.CreateKey(keyBuffer);

            var sigBuffer = CryptographicEngine.Sign(key, buffer);
            string signature = CryptographicBuffer.EncodeToBase64String(sigBuffer);
            return signature;
        }

        private string GenerateSignature(string httpMethod, string secret, string apiUrl, string parameters)
        {
            string encodedUrl = UrlHelper.Encode(apiUrl);
            string encodedParameters = UrlHelper.Encode(parameters);

            //generate the basestring
            string basestring = httpMethod + "&" + encodedUrl + "&" + encodedParameters;

            //hmac-sha1 encryption:
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            //create key (request_token can be an empty string)
            string key = consumerSecret + "&" + secret;
            string signature = Sha1Encrypt(basestring, key);

            //encode the signature to make it url safe and return the encoded url
            return UrlHelper.Encode(signature);
        }

        // Get request token
        public async Task<bool> GetRequestTokenAsync()
        {
            string timestamp = DateTimeUtils.GetTimestamp();
            string nonce = Guid.NewGuid().ToString().Replace("-", null);

            // Encode the request string
            string paramString = "oauth_callback=" + UrlHelper.Encode(CallbackUrl);
            paramString += "&oauth_consumer_key=" + consumerKey;
            paramString += "&oauth_nonce=" + nonce;
            paramString += "&oauth_signature_method=HMAC-SHA1";
            paramString += "&oauth_timestamp=" + timestamp;
            paramString += "&oauth_version=1.0";

            string signature = GenerateSignature("POST", null, BaseUrl + "/oauth/request_token", paramString);

            // Create the http request
            string requestUrl = BaseUrl + "/oauth/request_token?" + paramString + "&oauth_signature=" + signature;

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage resp = await client.PostAsync(requestUrl, null);
                resp.EnsureSuccessStatusCode();

                var result = await resp.Content.ReadAsStringAsync();

                if (result.Contains("oauth_callback_confirmed=true"))
                {
                    // Parse out the request token and secret
                    string[] parts = result.Split('&');
                    string tokenString = parts[0];
                    RequestToken = tokenString.Split('=')[1];

                    string secretString = parts[1];
                    RequestTokenSecret = secretString.Split('=')[1];

                    // Dispatch event
                    return true;
                }
                else
                {
                    Debug.WriteLine(result);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public async Task<bool> GetAccessTokenAsync()
        {
            string timestamp = DateTimeUtils.GetTimestamp();
            string nonce = Guid.NewGuid().ToString().Replace("-", null);

            // Encode the request string
            string paramString = "oauth_consumer_key=" + consumerKey;
            paramString += "&oauth_nonce=" + nonce;
            paramString += "&oauth_signature_method=HMAC-SHA1";
            paramString += "&oauth_timestamp=" + timestamp;
            paramString += "&oauth_token=" + RequestToken;
            paramString += "&oauth_verifier=" + RequestTokenVerifier;
            paramString += "&oauth_version=1.0";

            string signature = GenerateSignature("POST", RequestTokenSecret, BaseUrl + "/oauth/access_token", paramString);

            // Create the http request
            string requestUrl = BaseUrl + "/oauth/access_token?" + paramString + "&oauth_signature=" + signature;

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage resp = await client.PostAsync(requestUrl, null);
                resp.EnsureSuccessStatusCode();

                var result = await resp.Content.ReadAsStringAsync();
                var parts = result.Split('&');

                AccessToken = parts[0].Split('=')[1];
                AccessTokenSecret = parts[1].Split('=')[1];

                Debug.WriteLine(result);
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }


    }
}
