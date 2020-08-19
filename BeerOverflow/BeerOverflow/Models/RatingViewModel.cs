using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class RatingViewModel
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int BeerId { get; set; }
        public string Beer { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Please enter value between {0} and {1}")]
        public double RatingValue { get; set; }
    }
}
