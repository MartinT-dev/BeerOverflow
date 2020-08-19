using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Like
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public bool isLiked { get; set; }
        public bool isDisliked { get; set; }
    }
}
