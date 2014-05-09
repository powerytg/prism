using Prism.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public class PhotoGroupRendererBase : UserControl
    {
        public static readonly DependencyProperty PhotoGroupSourceProperty = DependencyProperty.Register(
        "PhotoGroupSource",
        typeof(PhotoGroup),
        typeof(PhotoGroupRendererBase),
        new PropertyMetadata(null, OPhotoGroupPropertyChanged));

        public PhotoGroup PhotoGroupSource
        {
            get { return (PhotoGroup)GetValue(PhotoGroupSourceProperty); }
            set { SetValue(PhotoGroupSourceProperty, value); }
        }

        private static void OPhotoGroupPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (PhotoGroupRendererBase)sender;
            target.OnPhotoGroupChanged();
        }

        protected virtual void OnPhotoGroupChanged()
        {
        }
    }
}
