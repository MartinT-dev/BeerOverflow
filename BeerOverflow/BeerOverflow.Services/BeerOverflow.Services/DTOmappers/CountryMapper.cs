using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class CountryMapper
    {
        public static CountryDTO GetDto(this Country entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new CountryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Breweries = entity.Breweries.GetDtos(),
                Beers = entity.Beers.GetDtos()
            };
        }

        public static ICollection<CountryDTO> GetDtos(this ICollection<Country> entities)
        {
            return entities.Select(GetDto).ToList();
        }

    }
}
