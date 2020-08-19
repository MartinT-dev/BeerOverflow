using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class BeerMapper 
    {
        public static BeerDTO GetDto(this Beer entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };

            return new BeerDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Abv = entity.Abv,
                CountryId = entity.CountryId,
                Country = entity.Country.Name,
                BreweryId = entity.BreweryId,
                Brewery = entity.Brewery.Name,
                StyleId = entity.StyleId,
                Style = entity.Style.Name,
                Rating = entity.Ratings
                    .Any() ? entity.Ratings
                    .Average(x => x.RatingValue) : 0.00,
                Ratings = entity.Ratings.GetDtos(),
                Reviews = entity.Reviews.GetDtos(),
            };
        }

        public static ICollection<BeerDTO> GetDtos(this ICollection<Beer> entities)
        {
            return entities.Select(GetDto).ToList();
        }

    }
}
