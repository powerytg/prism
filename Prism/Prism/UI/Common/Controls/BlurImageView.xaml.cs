using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Nokia.Graphics.Imaging;
using Nokia.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.Common.Controls
{
    public sealed partial class BlurImageView : UserControl
    {
        public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.Register(
        "ImageUrl",
        typeof(string),
        typeof(BlurImageView),
        new PropertyMetadata(null, OImageUrlPropertyChanged));

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        private static void OImageUrlPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (BlurImageView)sender;
            target.OnImageUrlChanged();
        }

        private async void OnImageUrlChanged()
        {
            if (ImageUrl == null)
            {
                return;
            }

            var rass = RandomAccessStreamReference.CreateFromUri(new Uri(ImageUrl, UriKind.Absolute));
            IRandomAccessStream stream = await rass.OpenReadAsync();
            var decoder = await BitmapDecoder.CreateAsync(stream);

            Rect bounds = Window.Current.Bounds;
            //WriteableBitmap bmp = new WriteableBitmap((int)bounds.Width, (int)bounds.Height);
            var displayWidth = Math.Min(decoder.PixelWidth, bounds.Width);
            var displayHeight = Math.Min(decoder.PixelHeight, bounds.Height);
            WriteableBitmap bmp = new WriteableBitmap((int)displayWidth, (int)displayHeight);

            stream.Seek(0);

            //var blurFilter = new BlurFilter(60);
            using (var source = new RandomAccessStreamImageSource(stream))
            using (var blurFilter = new LensBlurEffect(source, new LensBlurPredefinedKernel(LensBlurPredefinedKernelShape.Circle, 20)))
            //using (var filterEffect = new FilterEffect(source) { Filters = new[] { blurFilter } })
            using (var renderer = new WriteableBitmapRenderer(blurFilter, bmp))
            {
                

                bmp = await renderer.RenderAsync();
                bmp.Invalidate(); 
                BackgroundImage.Source = bmp;
            }
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BlurImageView()
        {
            this.InitializeComponent();
        }
    }
}
