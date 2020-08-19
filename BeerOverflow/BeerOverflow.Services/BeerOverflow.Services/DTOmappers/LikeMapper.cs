using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class LikeMapper
    {
        public static LikeDTO GetDto(this Like entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new LikeDTO
            {
                UserId = entity.UserId,
                User = entity.User.UserName,
                ReviewId = entity.ReviewId,
                Review = entity.Review.Title,
                isLiked=entity.isLiked,
                isDisliked=entity.isDisliked
            };
        }

        public static ICollection<LikeDTO> GetDtos(this ICollection<Like> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
