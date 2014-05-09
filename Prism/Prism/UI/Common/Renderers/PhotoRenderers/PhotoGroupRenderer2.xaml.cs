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

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public sealed partial class PhotoGroupRenderer2 : PhotoGroupRendererBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PhotoGroupRenderer2()
        {
            this.InitializeComponent();
        }

        protected override void OnPhotoGroupChanged()
        {
            base.OnPhotoGroupChanged();
            Renderer1.PhotoSource = PhotoGroupSource.Photos[0];
            Renderer1.StreamContext = PhotoGroupSource.StreamContext;
            Renderer1.UserId = PhotoGroupSource.UserId;
            Renderer1.UserName = PhotoGroupSource.UserName;

            Renderer2.PhotoSource = PhotoGroupSource.Photos[1];
            Renderer2.StreamContext = PhotoGroupSource.StreamContext;
            Renderer2.UserId = PhotoGroupSource.UserId;
            Renderer2.UserName = PhotoGroupSource.UserName;

            LayoutHorizontally();
        }

        private void LayoutHorizontally()
        {
            LayoutRoot.MaxHeight = 280;

            LayoutRoot.RowDefinitions.Clear();
            LayoutRoot.ColumnDefinitions.Clear();

            float ratio;
            int percent1, percent2;
            if (Renderer1.PhotoSource.Width > 0 && Renderer2.PhotoSource.Width > 0)
            {
                ratio = (float)Renderer1.PhotoSource.Width / (float)(Renderer1.PhotoSource.Width + Renderer2.PhotoSource.Width);
            }
            else
            {
                int f1 = Math.Min(Renderer1.PhotoSource.Width, Renderer1.PhotoSource.Height);
                int f2 = Math.Min(Renderer2.PhotoSource.Width, Renderer2.PhotoSource.Height);
                if (f1 != 0 && f2 != 0)
                {
                    ratio = (float)f1 / (float)(f1 + f2);
                }
                else
                {
                    ratio = 0.6f;
                }
            }

            if (ratio < 0.3f)
            {
                ratio = 0.3f;
            }

            if (ratio > 0.75f)
            {
                ratio = 0.75f;
            }

            percent1 = (int)Math.Floor(ratio * 100);
            percent2 = 100 - percent1;

            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(percent1, GridUnitType.Star) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(percent2, GridUnitType.Star) });

            Renderer1.SetValue(Grid.ColumnProperty, 0);
            Renderer2.SetValue(Grid.ColumnProperty, 1);
        }
    }
}
