using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? UserId { get; set; }
        public string User { get; set; }

        public int? BeerId { get; set; }
        public string Beer { get; set; }
        public int Liked { get; set; }

        public int Disliked { get; set; }

        public bool isDeleted { get; set; }

        public bool isFlagged { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }
    }
}
