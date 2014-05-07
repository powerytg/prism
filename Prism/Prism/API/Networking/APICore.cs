using Prism.API.Networking.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Networking
{
    public partial class APICore
    {
        #region Events

        public EventHandler<APIEventArgs> GetCurrentUserInfoComplete;
        public EventHandler GetCurrentUserInfoFailed;

        #endregion

        private static volatile APICore instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static APICore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new APICore();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public APICore()
        {
            // Ignore
        }

    }
}
