using Prism.API.Storage.Models;
using Prism.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.UI.Common.Renderers.PhotoRenderers
{
    public class CommonPhotoGroupFactory
    {
        /// <summary>
        /// Name of the stream that this photo group belongs to
        /// </summary>
        public string StreamContext { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string UserName { get; set; }

        // Random generator
        private Random randomGenerator = new Random();

        public List<PhotoGroup> GeneratePhotoGroups(List<Photo> photos)
        {
            List<PhotoGroup> result = new List<PhotoGroup>();

            // Randomly slice the photo into groups
            int min = 1;
            int max = 4;
            int position = 0;

            while (position < photos.Count)
            {
                int ranNum = randomGenerator.Next(min, max);
                List<Photo> group = new List<Photo>();

                if (position + ranNum >= photos.Count)
                {
                    for (int i = position; i < photos.Count; i++)
                    {
                        group.Add(photos[i]);
                    }

                    result.Add(new PhotoGroup(group, StreamContext, UserId, UserName));
                    break;
                }

                for (int i = position; i < position + ranNum; i++)
                {
                    group.Add(photos[i]);
                }

                result.Add(new PhotoGroup(group, StreamContext, UserId, UserName));
                position += ranNum;
            }

            return result;
        }

    }
}
