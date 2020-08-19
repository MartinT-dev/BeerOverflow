using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        [DisplayName("Title")]
        //[Required]
        [StringLength(20, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Title { get; set; }
        [DisplayName("Text")]
        //[Required]
        [StringLength(500, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Text { get; set; }
        public int? UserId { get; set; }
        public string User { get; set; }

        public int? BeerId { get; set; }
        public string Beer { get; set; }
        public int Liked { get; set; }

        public int Disliked { get; set; }

        public bool isFlagged { get; set; }
    }
}
