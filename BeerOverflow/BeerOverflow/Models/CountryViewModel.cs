using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Country Name")]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        public ICollection<BreweryViewModel> Breweries { get; set; }

        public ICollection<BeerViewModel> Beers { get; set; }
    }
}
