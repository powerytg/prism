using Nokia.Graphics.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.ProCam
{
    public sealed partial class BlurCameraBackgroundView : UserControl
    {
        private CameraPreviewImageSource cameraPreviewImageSource; // Using camera as our image source
        private WriteableBitmap outputBitmap; // Target for our renderer
        private FilterEffect effect; // Filter to be used on the camera image
        private WriteableBitmapRenderer renderer;
        public bool Initialized { get; set; }
        private bool isRendering; // Used to prevent multiple renderers running at once

        public async Task InitializeAsync()
        {
            // Discover all the cameras
            await EnumerateCamerasAsync();
            
            // Create a camera preview image source (from Imaging SDK)
            cameraPreviewImageSource = new CameraPreviewImageSource();
            await cameraPreviewImageSource.InitializeAsync(backCamera.Id);
            var properties = await cameraPreviewImageSource.StartPreviewAsync();

            VideoDeviceController controller = (VideoDeviceController)cameraPreviewImageSource.VideoDeviceController;            

            // Create a preview bitmap with the correct aspect ratio
            var width = properties.Width;
            var height = properties.Height;
            var bitmap = new WriteableBitmap((int)width, (int)height);

            outputBitmap = bitmap;

            // Create a filter effect to be used with the source (no filters yet)
            //effect = new LensBlurEffect(cameraPreviewImageSource, new LensBlurPredefinedKernel(LensBlurPredefinedKernelShape.Circle, 50));
            effect = new FilterEffect(cameraPreviewImageSource);

            var blur = new BlurFilter();
            blur.KernelSize = 30;
            blur.BlurRegionShape = BlurRegionShape.Elliptical;
            effect.Filters = new[] { blur };

            renderer = new WriteableBitmapRenderer(effect, outputBitmap);

            Initialized = true;

            CaptureImage.Source = outputBitmap;

            // Attach preview frame delegate
            cameraPreviewImageSource.PreviewFrameAvailable += OnPreviewFrameAvailable;
        }

        public async Task PausePreviewAsync()
        {
            if (Initialized)
            {
                await cameraPreviewImageSource.StopPreviewAsync();
            }
        }

        public async Task ResumePreviewAsync()
        {
            if (Initialized)
            {
                await cameraPreviewImageSource.InitializeAsync(string.Empty);
                await cameraPreviewImageSource.StartPreviewAsync();
            }
        }

        /// <summary>
        /// Render a frame with the selected filter
        /// </summary>
        private async void OnPreviewFrameAvailable(IImageSize args)
        {
            // Prevent multiple rendering attempts at once
            if (Initialized && !isRendering)
            {
                isRendering = true;

                // Render the image with the filter
                await renderer.RenderAsync();

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    () =>
                    {
                        outputBitmap.Invalidate();
                    });

                isRendering = false;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BlurCameraBackgroundView()
        {
            this.InitializeComponent();
        }
    }
}
