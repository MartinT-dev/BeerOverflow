using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class WishlistMapper
    {
        public static WishlistDTO GetDto(this WishList entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new WishlistDTO
            {
                UserId = entity.UserId,
                User = entity.User.UserName,
                BeerId = entity.BeerId,
                Beer = entity.Beer.Name
            };
        }

        public static ICollection<WishlistDTO> GetDtos(this ICollection<WishList> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
