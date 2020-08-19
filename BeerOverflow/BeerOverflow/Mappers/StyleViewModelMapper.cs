using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class StyleViewModelMapper
    {
        public static StyleViewModel GetViewModel(this StyleDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new StyleViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Beers = dto.Beers.GetViewModels()
            };
        }

        public static ICollection<StyleViewModel> GetViewModels(this ICollection<StyleDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }

        public static StyleDTO GetDTO(this StyleViewModel vm)
        {
            if (vm == null)
            {
                throw new ArgumentException();
            };
            return new StyleDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                //Beers = vm.Beers.GetDTOs()
            };
        }
        public static ICollection<StyleDTO> GetDTOs(this ICollection<StyleViewModel> vms)
        {
            return vms.Select(GetDTO).ToList();
        }
    }
}
