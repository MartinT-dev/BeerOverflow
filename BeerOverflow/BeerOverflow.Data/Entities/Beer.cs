using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Beer
    {
        public Beer()
        {
            this.Reviews = new List<Review>();
            this.Ratings = new List<Rating>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        public double Abv { get; set; }
        public int? BreweryId { get; set; }
        public Brewery Brewery { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? StyleId { get; set; }
        public Style Style { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<DrankList> DrankLists { get; set; }

    }
}
