using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class ReviewMapper
    {
        public static ReviewDTO GetDto(this Review entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };
            return new ReviewDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Text = entity.Text,
                UserId = entity.UserId,
                User = entity.User.UserName,
                BeerId = entity.BeerId,
                Beer=entity.Beer.Name,
                Liked = entity.Likes
                    .Where(x=>x.isLiked == true)
                    .Count(),
                Disliked = entity.Likes
                    .Where(x => x.isDisliked == true)
                    .Count(),
                isFlagged = entity.isFlagged,
                isDeleted = entity.isDeleted,
                Likes=entity.Likes.GetDtos()
            };
        }

        public static ICollection<ReviewDTO> GetDtos(this ICollection<Review> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
