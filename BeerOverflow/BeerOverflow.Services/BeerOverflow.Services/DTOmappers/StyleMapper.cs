using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class StyleMapper
    {
        public static StyleDTO GetDto(this Style entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new StyleDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Beers = entity.Beers.GetDtos()
            };
        }

        public static ICollection<StyleDTO> GetDtos(this ICollection<Style> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
