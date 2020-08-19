using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class BeerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Beer Name")]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }
        [DisplayName("Alcohol By Volume")]
        [Required]
        public double Abv { get; set; }
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public int? BreweryId { get; set; }
        public string Brewery { get; set; }
        public int? StyleId { get; set; }

        public string Style { get; set; }

        public double Rating { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; }

        public List<SelectListItem> Breweries { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Styles { get; set; }
    }
}
