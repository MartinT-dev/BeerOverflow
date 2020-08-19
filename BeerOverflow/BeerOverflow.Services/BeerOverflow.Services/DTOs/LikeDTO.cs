using BeerOverflow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class LikeDTO
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int ReviewId { get; set; }
        public string Review { get; set; }
        public bool isLiked { get; set; }
        public bool isDisliked { get; set; }
    }
}
