using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Rating
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        [Required]
        [Range (1,5)]
        public double RatingValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
