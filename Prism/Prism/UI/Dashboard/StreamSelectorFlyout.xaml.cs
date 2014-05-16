using Prism.API.Storage;
using Prism.API.Storage.Models;
using Prism.UI.Events;
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

namespace Prism.UI.Dashboard
{
    public sealed partial class StreamSelectorFlyout : UserControl
    {
        // Events
        public EventHandler<StreamChangedEventArgs> StreamChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        public StreamSelectorFlyout()
        {
            this.InitializeComponent();
        }

        private void PopularButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectStream(StorageCore.Instance.PopularStream);
        }

        private void EditorChoiceButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectStream(StorageCore.Instance.EditorChoiceStream);
        }

        private void TodayButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectStream(StorageCore.Instance.TodayStream);
        }

        private void WeekButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectStream(StorageCore.Instance.WeekStream);
        }

        private void UserButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectStream(StorageCore.Instance.CurrentUser.PhotoStream);
        }

        private void SelectStream(FeatureStream stream)
        {
            if (StreamChanged != null)
            {
                var evt = new StreamChangedEventArgs();
                evt.SelectedStream = stream;

                StreamChanged(this, evt);
            }
        }

    }
}
