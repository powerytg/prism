using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.UI.Models
{
    public class PhotoGroup
    {
        public List<Photo> Photos { get; set; }

        /// <summary>
        /// Name of the stream that this photo group belongs to
        /// </summary>
        public string StreamContext { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PhotoGroup()
            : base()
        {
            Photos = new List<Photo>();
        }

        public PhotoGroup(List<Photo> photos, string context, string userId, string userName)
        {
            Photos = new List<Photo>();
            Photos.AddRange(photos);

            StreamContext = context;
            UserId = userId;
            UserName = userName;
        }

    }
}
