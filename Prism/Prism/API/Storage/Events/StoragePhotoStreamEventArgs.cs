using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Events
{
    public class StoragePhotoStreamEventArgs : EventArgs
    {
        public FeatureStream Stream { get; set; }
        public int Page { get; set; }
        public int PhotoCount { get; set; }

        public List<Photo> NewPhotos { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public StoragePhotoStreamEventArgs()
        {
            NewPhotos = new List<Photo>();
        }
    }
}
