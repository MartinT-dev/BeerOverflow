using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class ReviewViewModelMapper
    {
        public static ReviewViewModel GetViewModel(this ReviewDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new ReviewViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Text = dto.Text,
                UserId = dto.UserId,
                User = dto.User.Split("@")[0],
                BeerId = dto.BeerId,
                Beer = dto.Beer,
                Liked = dto.Liked,
                Disliked = dto.Disliked,
                isFlagged = dto.isFlagged
            };
        }

        public static ICollection<ReviewViewModel> GetViewModels(this ICollection<ReviewDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }

        public static ReviewDTO GetDTO(this ReviewViewModel vm)
        {
            if (vm == null)
            {
                throw new ArgumentException();
            };
            return new ReviewDTO
            {
                Id = vm.Id,
                Title = vm.Title,
                Text = vm.Text,
                UserId = vm.UserId,
                User = vm.User,
                BeerId = vm.BeerId,
                Beer = vm.Beer,
                Liked = vm.Liked,
                Disliked = vm.Disliked,
                isFlagged = vm.isFlagged
            };
        }
        public static ICollection<ReviewDTO> GetDTOs(this ICollection<ReviewViewModel> vms)
        {
            return vms.Select(GetDTO).ToList();
        }
    }
}
