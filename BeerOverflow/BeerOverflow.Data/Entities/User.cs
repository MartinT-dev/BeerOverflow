using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.Reviews = new List<Review>();
            this.Ratings = new List<Rating>();
            this.WishLists = new List<WishList>();
            this.DrankLists = new List<DrankList>();
            this.Likes = new List<Like>();
        }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<DrankList> DrankLists { get; set; }
        public ICollection<Like> Likes { get; set; }

        public bool isBanned { get; set; }
    }
}
