using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace Prism.UI.ProCam
{
    public partial class CameraBackgroundView
    {
        private DeviceInformationCollection discoveredCameras;
        private DeviceInformation backCamera;
        private DeviceInformation frontCamera;

        private async Task EnumerateCamerasAsync()
        {
            try
            {
                discoveredCameras = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

                if (discoveredCameras == null || discoveredCameras.Count == 0)
                {
                    Debug.WriteLine("No cameras found!");
                    return;
                }

                // Iterate through the devices to discover which is front camera and which is back camera
                foreach (var dev in discoveredCameras)
                {
                    var location = dev.EnclosureLocation;
                    if (location != null)
                    {
                        if (location.Panel == Panel.Back)
                        {
                            backCamera = dev;
                            Debug.WriteLine("Back camera found: " + dev.Name);
                        }
                        
                        if (location.Panel == Panel.Front)
                        {
                            frontCamera = dev;
                            Debug.WriteLine("Front camera found: " + dev.Name);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
    }
}
