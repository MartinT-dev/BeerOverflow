using BeerOverflow.Areas.Admin.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Areas.Admin.Mappers
{
    public static class ReviewAdminMapper
    {
        public static ReviewAdminViewModel GetViewModel(this ReviewDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new ReviewAdminViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Text = dto.Text,
                User = dto.User,
                Beer = dto.Beer,
                isDeleted = dto.isDeleted,
                isFlagged = dto.isFlagged
            };
        }

        public static ICollection<ReviewAdminViewModel> GetViewModels(this ICollection<ReviewDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }
    }
}
