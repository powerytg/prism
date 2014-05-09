using Prism.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public class CommonPhotoGroupRendererSelector : DataTemplateSelector
    {
        public DataTemplate Renderer1 { get; set; }
        public DataTemplate Renderer2 { get; set; }
        public DataTemplate Renderer3 { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            PhotoGroup photoGroup = item as PhotoGroup;

            if (photoGroup.Photos.Count == 1)
            {
                return Renderer1;
            }
            else if (photoGroup.Photos.Count == 2)
            {
                return Renderer2;
            }
            else if (photoGroup.Photos.Count == 3)
            {
                return Renderer3;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
