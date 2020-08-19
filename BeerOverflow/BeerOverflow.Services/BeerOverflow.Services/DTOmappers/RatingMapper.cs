using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class RatingMapper
    {
        public static RatingDTO GetDto(this Rating entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new RatingDTO
            {
                UserId = entity.UserId,
                User = entity.User.UserName,
                BeerId = entity.BeerId,
                Beer = entity.Beer.Name,
                RatingValue = entity.RatingValue,
            };
        }

        public static ICollection<RatingDTO> GetDtos(this ICollection<Rating> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
