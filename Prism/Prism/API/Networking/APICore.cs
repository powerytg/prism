using Prism.API.Networking.Events;
using Prism.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Networking
{
    public partial class APICore
    {
        #region Events

        public EventHandler<APIEventArgs> GetCurrentUserInfoComplete;
        public EventHandler GetCurrentUserInfoFailed;

        public EventHandler<PhotoStreamEventArgs> GetPhotoStreamComplete;
        public EventHandler<PhotoStreamEventArgs> GetPhotoStreamFailed;

        #endregion

        public static int PerPage = 20;
        private string BaseUrl = "https://api.500px.com/v1";

        public List<KeyValuePair<string, string>> DefaultPhotoParameters { get; set; }

        private static volatile APICore instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static APICore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new APICore();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public APICore()
        {
            DefaultPhotoParameters = new List<KeyValuePair<string, string>>();
            DefaultPhotoParameters.Add(new KeyValuePair<string, string>(UrlHelper.Encode("image_size[]"), "1"));
            DefaultPhotoParameters.Add(new KeyValuePair<string, string>(UrlHelper.Encode("image_size[]"), "2"));
            DefaultPhotoParameters.Add(new KeyValuePair<string, string>(UrlHelper.Encode("image_size[]"), "3"));
            DefaultPhotoParameters.Add(new KeyValuePair<string, string>(UrlHelper.Encode("image_size[]"), "4"));
            DefaultPhotoParameters.Add(new KeyValuePair<string, string>("include_states", "1"));
        }

    }
}
