using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class BreweryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Brewery Name")]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        public int? CountryId { get; set; }

        public string Country { get; set; }

        public ICollection<BeerViewModel> Beers { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}
