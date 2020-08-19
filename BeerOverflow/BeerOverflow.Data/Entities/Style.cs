using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class Style
    {
        public Style()
        {
            this.Beers = new List<Beer>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}
