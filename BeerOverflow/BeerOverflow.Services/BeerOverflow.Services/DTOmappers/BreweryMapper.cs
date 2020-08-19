using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class BreweryMapper
    {
        public static BreweryDTO GetDto(this Brewery entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new BreweryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                CountryId = entity.CountryId,
                Country = entity.Country.Name,
                Beers = entity.Beers.GetDtos()
            };
        }

        public static ICollection<BreweryDTO> GetDtos(this ICollection<Brewery> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
