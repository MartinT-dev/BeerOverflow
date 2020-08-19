using BeerOverflow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class BreweryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int? CountryId { get; set; }

        public string Country { get; set; }

        public ICollection<BeerDTO> Beers { get; set; }
    }
}
