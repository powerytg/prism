using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Models
{
    public class FeatureStream
    {
        public List<Photo> Photos { get; set; }

        public string Name { get; set; }

        // Optional user id
        public string UserId { get; set; }

        // Optional username
        public string UserName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FeatureStream(string name = null)
        {
            Name = name;
            Photos = new List<Photo>();
        }
    }
}
