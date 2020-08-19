using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }

        public bool isBanned { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }
        public ICollection<WishlistDTO> WishLists { get; set; }
        public ICollection<DranklistDTO> DrankLists { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }
    }
}
