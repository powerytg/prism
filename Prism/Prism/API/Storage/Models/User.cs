using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Models
{
    public class User : ModelBase
    {
        public static int NoUploadLimit = -1;

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }
        public string SmallAvatarUrl { get; set; }
        public string LargeAvatarUrl { get; set; }
        public string TinyAvatarUrl { get; set; }
        public string DefaultAvatarUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string About { get; set; }
        public string Domain { get; set; }

        public int UpgradeStatus { get; set; }
        public bool ShowNude { get; set; }

        public Dictionary<string, string> ContactInfo { get; set; }
        public string Email { get; set; }

        public List<string> Cameras { get; set; }
        public List<string> Lenses { get; set; }

        public int PhotoCount { get; set; }
        public int Affection { get; set; }
        public int FavCount { get; set; }
        public int FriendCount { get; set; }
        public int FollowerCount { get; set; }

        public int UploadLimit { get; set; }
        public DateTime UploadLimitExpiration { get; set; }

        public int UpgradeType { get; set; }
        public DateTime UpgradeStatusExpiration { get; set; }

        public FeatureStream PhotoStream { get; set; }
        public FeatureStream FriendStream { get; set; }
        public FeatureStream FavStream { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public User()
            : base()
        {
            ContactInfo = new Dictionary<string, string>();
            Cameras = new List<string>();
            Lenses = new List<string>();

            PhotoStream = new FeatureStream("user");
            FriendStream = new FeatureStream("user_friends");
            FavStream = new FeatureStream("user_favourites");
        }
    }
}
