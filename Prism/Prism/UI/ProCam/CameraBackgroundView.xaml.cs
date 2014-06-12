using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.UI.ProCam
{
    public sealed partial class CameraBackgroundView : UserControl
    {
        private MediaCapture captureManager;
        public bool Initialized { get; set; }
        public bool IsPreviewing { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CameraBackgroundView()
        {
            this.InitializeComponent();
        }

        public async Task PausePreviewAsync()
        {
            await CleanupCaptureResources();
        }

        public async Task ResumePreviewAsync()
        {
            if (Initialized)
            {
                await captureManager.InitializeAsync();
                await captureManager.StartPreviewAsync();
            }
        }

        public async Task InitializeAsync()
        {
            // Discover all the cameras
            await EnumerateCamerasAsync();

            // Create a camera preview image source (from Imaging SDK)
            captureManager = new MediaCapture();
            MediaCaptureInitializationSettings captureSettings = new MediaCaptureInitializationSettings();
            captureSettings.VideoDeviceId = backCamera.Id;

            await captureManager.InitializeAsync(captureSettings);

            CameraPreview.Source = captureManager;
            captureManager.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
            await captureManager.StartPreviewAsync();
        }

        public async Task CleanupCaptureResources()
        {
            if (IsPreviewing && captureManager != null)
            {
                await captureManager.StopPreviewAsync();
                IsPreviewing = false;
            }

            if (captureManager != null)
            {
                if (CameraPreview != null)
                {
                    CameraPreview.Source = null;
                }

                captureManager.Dispose();
            }
        }

    }
}
