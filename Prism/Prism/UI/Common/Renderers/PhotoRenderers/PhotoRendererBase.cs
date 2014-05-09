using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public class PhotoRendererBase : UserControl
    {
        /// <summary>
        /// Name of the stream that this photo group belongs to
        /// </summary>
        public string StreamContext { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserName { get; set; }

        public static readonly DependencyProperty PhotoSourceProperty = DependencyProperty.Register(
        "PhotoSource",
        typeof(Photo),
        typeof(PhotoRendererBase),
        new PropertyMetadata(null, OPhotoSourcePropertyChanged));

        public Photo PhotoSource
        {
            get { return (Photo)GetValue(PhotoSourceProperty); }
            set { SetValue(PhotoSourceProperty, value); }
        }

        private static void OPhotoSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (PhotoRendererBase)sender;
            target.OnPhotoSourceChanged();
        }

        protected virtual void OnPhotoSourceChanged()
        {
        }
    }
}
