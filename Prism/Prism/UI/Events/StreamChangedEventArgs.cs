using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.UI.Events
{
    public class StreamChangedEventArgs : EventArgs
    {
        public FeatureStream SelectedStream { get; set; }
    }
}
