using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BreweryDTO> Breweries { get; set; }

        public ICollection<BeerDTO> Beers { get; set; }
    }
}
