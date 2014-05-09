using Prism.API.Networking;
using Prism.API.Storage.Events;
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

        // User
        public EventHandler CurrentUserInfoUpdated;

        // Photo stream
        public EventHandler<StoragePhotoStreamEventArgs> PhotoStreamUpdated;

        #endregion

        public Dictionary<int, string> CategoryTable { get; set; }
        public User CurrentUser { get; set; }
        public Dictionary<string, User> UserCache { get; set; }
        public List<User> UserList { get; set; }

        public Dictionary<string, Photo> PhotoCache { get; set; }

        // Feature streams
        public FeatureStream PopularStream { get; set; }
        public FeatureStream UpcomingStream { get; set; }
        public FeatureStream TodayStream { get; set; }
        public FeatureStream YesterdayStream { get; set; }
        public FeatureStream WeekStream { get; set; }
        public FeatureStream EditorChoiceStream { get; set; }

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

            PhotoCache = new Dictionary<string, Photo>();

            // Category
            InitializePhotoCategoryTable();
            
            // Feature streams
            PopularStream = new FeatureStream("popular");
            UpcomingStream = new FeatureStream("upcoming");
            EditorChoiceStream = new FeatureStream("editors");
            TodayStream = new FeatureStream("fresh_today");
            YesterdayStream = new FeatureStream("fresh_yesterday");
            WeekStream = new FeatureStream("fresh_week");

            // Events
            // User
            APICore.Instance.GetCurrentUserInfoComplete += OnCurrentUserInfoRetrieved;

            // Photo stream
            APICore.Instance.GetPhotoStreamComplete += OnPhotoStreamReturned;
        }

    }
}
