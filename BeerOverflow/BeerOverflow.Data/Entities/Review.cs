using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Review
    {
        public Review()
        {
            this.Likes = new List<Like>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Text { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public int? BeerId { get; set; }
        public Beer Beer { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }
        public bool isFlagged { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
