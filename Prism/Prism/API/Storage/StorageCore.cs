using Prism.API.Networking;
using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage
{
    public partial class StorageCore
    {
        #region Events

        public EventHandler CurrentUserInfoUpdated;

        #endregion

        public User CurrentUser { get; set; }
        public Dictionary<string, User> UserCache { get; set; }
        public List<User> UserList { get; set; }

        private static volatile StorageCore instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static StorageCore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new StorageCore();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StorageCore()
        {
            UserCache = new Dictionary<string, User>();
            UserList = new List<User>();

            // Events
            APICore.Instance.GetCurrentUserInfoComplete += OnCurrentUserInfoRetrieved;
        }

    }
}
