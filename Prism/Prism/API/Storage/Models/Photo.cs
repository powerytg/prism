using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage.Models
{
    public class Photo : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ViewCount { get; set; }
        public int VoteCount { get; set; }
        public int FavCount { get; set; }
        public int CommentCount { get; set; }
        public double Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Category { get; set; }
        public bool IsPrivate { get; set; }
        
        public bool NSFW { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string ImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public string MediumImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string TinyImageUrl { get; set; }

        public string UserId { get; set; }

        public bool Voted { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
        public bool Faved { get; set; }

        public string HighResUrl
        {
            get
            {
                if (LargeImageUrl != null)
                {
                    return LargeImageUrl;
                }
                else if (MediumImageUrl != null)
                {
                    return MediumImageUrl;
                }
                else
                {
                    return ImageUrl;
                }
            }
        }
    }
}
