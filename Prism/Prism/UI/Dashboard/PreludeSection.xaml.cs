﻿using Prism.API.Storage;
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
    public sealed partial class PreludeSection : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PreludeSection()
        {
            this.InitializeComponent();

            StorageCore.Instance.CurrentUserInfoUpdated += OnCurrentInfoUpdated;
        }

        private void OnCurrentInfoUpdated(object sender, EventArgs e)
        {
            
        }

    }
}
