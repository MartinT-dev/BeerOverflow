using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        Task<CountryDTO> GetCountryAsync(int id);
        Task<CountryDTO> UpdateCountryAsync(int id, string newName);
        Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO);
        Task<ICollection<CountryDTO>> GetAllCountriesAsync();
        Task DeleteCountryAsync(int id);
    }
}
