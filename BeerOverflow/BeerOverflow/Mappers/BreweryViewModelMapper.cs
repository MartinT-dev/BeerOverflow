using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class BreweryViewModelMapper
    {
        public static BreweryViewModel GetViewModel(this BreweryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new BreweryViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                CountryId = dto.CountryId,
                Country = dto.Country,
                Beers = dto.Beers.GetViewModels()
            };
        }

        public static ICollection<BreweryViewModel> GetViewModels(this ICollection<BreweryDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }

        public static BreweryDTO GetDTO(this BreweryViewModel vm)
        {
            if (vm == null)
            {
                throw new ArgumentException();
            };
            return new BreweryDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                CountryId = vm.CountryId,
                Country = vm.Country,
                //Beers = vm.Beers.GetDTOs()
            };
        }

        public static ICollection<BreweryDTO> GetDTOs(this ICollection<BreweryViewModel> vms)
        {
            return vms.Select(GetDTO).ToList();
        }
    }
}
