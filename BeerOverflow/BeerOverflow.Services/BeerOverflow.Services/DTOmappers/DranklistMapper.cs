using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class DranklistMapper
    {
        public static DranklistDTO GetDto(this DrankList entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new DranklistDTO
            {
                UserId = entity.UserId,
                User = entity.User.UserName,
                BeerId = entity.BeerId,
                Beer = entity.Beer.Name
            };
        }

        public static ICollection<DranklistDTO> GetDtos(this ICollection<DrankList> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
