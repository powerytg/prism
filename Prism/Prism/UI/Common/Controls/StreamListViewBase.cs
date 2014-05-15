using Prism.API.Storage;
using Prism.API.Storage.Events;
using Prism.API.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Prism.UI.Common.Controls
{
    public class StreamListViewBase : UserControl
    {
        // Events
        public EventHandler LoadingStarted;
        public EventHandler LoadingComplete;

        public static readonly DependencyProperty StreamProperty = DependencyProperty.Register(
        "Stream",
        typeof(FeatureStream),
        typeof(StreamListViewBase),
        new PropertyMetadata(null, OStreamPropertyChanged));

        public FeatureStream Stream
        {
            get { return (FeatureStream)GetValue(StreamProperty); }
            set { SetValue(StreamProperty, value); }
        }

        private static void OStreamPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (StreamListViewBase)sender;
            target.OnStreamChanged();
        }

        protected virtual void OnStreamChanged()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StreamListViewBase()
            : base()
        {
            
        }

    }
}
