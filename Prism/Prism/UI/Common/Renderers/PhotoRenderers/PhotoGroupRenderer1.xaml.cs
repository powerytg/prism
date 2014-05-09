using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public sealed partial class PhotoGroupRenderer1 : PhotoGroupRendererBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PhotoGroupRenderer1()
        {
            this.InitializeComponent();
        }

        protected override void OnPhotoGroupChanged()
        {
            base.OnPhotoGroupChanged();
            Renderer.PhotoSource = PhotoGroupSource.Photos[0];
            Renderer.StreamContext = PhotoGroupSource.StreamContext;
            Renderer.UserId = PhotoGroupSource.UserId;
            Renderer.UserName = PhotoGroupSource.UserName;

        }
    }
}
