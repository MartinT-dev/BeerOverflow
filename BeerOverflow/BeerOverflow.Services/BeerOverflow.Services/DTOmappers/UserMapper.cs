using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.DTOmappers
{
    public static class UserMapper
    {
        public static UserDTO GetDto(this User entity)
        {
            if (entity == null)
            {
                throw new ArgumentException();
            };

            return new UserDTO
            {
                Id = entity.Id,
                Username = entity.UserName,
                Country = entity.Country,
                Ratings = entity.Ratings.GetDtos(),
                Reviews = entity.Reviews.GetDtos(),
                WishLists = entity.WishLists.GetDtos(),
                DrankLists = entity.DrankLists.GetDtos(),
                Likes = entity.Likes.GetDtos(),
                isBanned = entity.isBanned
            };
        }

        public static ICollection<UserDTO> GetDtos(this ICollection<User> entities)
        {
            return entities.Select(GetDto).ToList();
        }
    }
}
