using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Models
{
    public class Photo : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ViewCount { get; set; }
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Category { get; set; }
    }
}
