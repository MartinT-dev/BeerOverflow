using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBreweryService
    {
        Task<BreweryDTO> GetBreweryAsync(int id);
        Task<BreweryDTO> UpdateBreweryAsync(int id, string newName);
        Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO);
        Task<ICollection<BreweryDTO>> GetAllBreweriesAsync();
        Task DeleteBreweryAsync(int id);
    }
}
