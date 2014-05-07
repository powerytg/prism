using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Networking.Events
{
    public class APIEventArgs : EventArgs
    {
        public string Result { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public APIEventArgs() : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result"></param>
        public APIEventArgs(string result) : base()
        {
            this.Result = result;
        }

    }
}
