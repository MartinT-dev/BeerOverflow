using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class BeerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Abv { get; set; }
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public int? BreweryId { get; set; }
        public string Brewery { get; set; }
        public int? StyleId { get; set; }
        public string Style { get; set; }

        public double Rating { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }

    }
}
