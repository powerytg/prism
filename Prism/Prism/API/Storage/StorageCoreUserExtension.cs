using Prism.API.Networking.Events;
using Prism.API.Storage.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage
{
    public partial class StorageCore
    {
        private void OnCurrentUserInfoRetrieved(object sender, APIEventArgs e)
        {
            CurrentUser = UserFactory.UserWithJson(e.Result);

            if (CurrentUserInfoUpdated != null)
            {
                CurrentUserInfoUpdated(this, null);
            }            
        }

    }
}
