using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Brewery
    {
        public Brewery()
        {
            this.Beers = new List<Beer>();
        }  
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<Beer> Beers { get; set; }
    }
}
