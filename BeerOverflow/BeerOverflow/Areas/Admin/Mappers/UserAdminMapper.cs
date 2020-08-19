using BeerOverflow.Areas.Admin.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Areas.Admin.Mappers
{
    public static class UserAdminMapper
    {
        public static UserAdminViewModel GetViewModel(this UserDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new UserAdminViewModel
            {
                Id = dto.Id,
                Username = dto.Username,
                Country = dto.Country,
                isBanned = dto.isBanned
            };
        }

        public static ICollection<UserAdminViewModel> GetViewModels(this ICollection<UserDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }
    }
}
