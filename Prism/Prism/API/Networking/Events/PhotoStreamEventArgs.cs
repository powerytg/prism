using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Networking.Events
{
    public class PhotoStreamEventArgs : EventArgs
    {
        public FeatureStream Stream { get; set; }
        public String Result { get; set; }

        public PhotoStreamEventArgs(FeatureStream stream, String result = null)
        {
            Stream = stream;
            Result = result;
        }
    }
}
