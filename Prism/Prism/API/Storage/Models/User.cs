using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Models
{
    public class User : ModelBase
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

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
        public DateTime UpgradeStatusExpiration { get; set; }
    }
}
