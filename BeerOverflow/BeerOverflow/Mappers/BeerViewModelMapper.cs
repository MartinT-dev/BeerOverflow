using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mappers
{
    public static class BeerViewModelMapper
    {
        public static BeerViewModel GetViewModel(this BeerDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException();
            };
            return new BeerViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Abv = dto.Abv,
                CountryId = dto.CountryId,
                Country = dto.Country,
                BreweryId = dto.BreweryId,
                Brewery = dto.Brewery,
                StyleId = dto.StyleId,
                Style = dto.Style,
                Rating = Math.Round(dto.Rating,2),
                Reviews = dto.Reviews.GetViewModels()
            };
        }

        public static ICollection<BeerViewModel> GetViewModels(this ICollection<BeerDTO> dtos)
        {
            return dtos.Select(GetViewModel).ToList();
        }

        public static BeerDTO GetDTO(this BeerViewModel vm)
        {
            if (vm == null)
            {
                throw new ArgumentException();
            };
            return new BeerDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                Abv = vm.Abv,
                CountryId = vm.CountryId,
                Country = vm.Country,
                BreweryId = vm.BreweryId,
                Brewery = vm.Brewery,
                StyleId = vm.StyleId,
                Style = vm.Style,
                Rating = vm.Rating,
                //Reviews = vm.Reviews.GetDTOs()
            };
        }

        public static ICollection<BeerDTO> GetDTOs(this ICollection<BeerViewModel> vms)
        {
            return vms.Select(GetDTO).ToList();
        }
    }
}
