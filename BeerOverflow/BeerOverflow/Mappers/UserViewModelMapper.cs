using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class UserViewModelMapper
    {
        public static UserViewModel GetViewModel(this UserDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new UserViewModel
            {
                Id = dto.Id,
                Username = dto.Username,
                Country = dto.Country,
                WishLists = dto.WishLists.Select(x => x.Beer).ToList(),
                DrankLists = dto.DrankLists.Select(x => x.Beer).ToList()
            };
        }

        public static ICollection<UserViewModel> GetViewModels(this ICollection<UserDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }
    }
}
