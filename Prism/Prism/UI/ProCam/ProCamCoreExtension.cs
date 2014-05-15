using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace Prism.UI.ProCam
{
    public partial class ProCamPage
    {
        private MediaCapture captureManager = null;

        private async void InitializeCamera()
        {
            if (captureManager == null)
            {
                captureManager = new MediaCapture();
                await captureManager.InitializeAsync();

                PhotoCaptureElement.Source = captureManager;
                await captureManager.StartPreviewAsync();
            }
        }
    }
}
