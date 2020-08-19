using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class CountryViewModelMapper
    {
        public static CountryViewModel GetViewModel(this CountryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new CountryViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Breweries = dto.Breweries.GetViewModels(),
                Beers = dto.Beers.GetViewModels()
            };
        }

        public static ICollection<CountryViewModel> GetViewModels(this ICollection<CountryDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }

        public static CountryDTO GetDTO(this CountryViewModel vm)
        {
            if (vm == null)
            {
                throw new ArgumentException();
            };
            return new CountryDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                //Breweries = vm.Breweries.GetDTOs(),
                //Beers = vm.Beers.GetDTOs()
            };
        }
        public static ICollection<CountryDTO> GetDTOs(this ICollection<CountryViewModel> vms)
        {
            return vms.Select(GetDTO).ToList();
        }

    }
}
