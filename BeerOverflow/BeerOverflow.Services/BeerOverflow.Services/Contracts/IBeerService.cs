using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        Task<BeerDTO> GetBeerAsync(int id);
        Task<ICollection<BeerDTO>> GetAllBeersAsync();
        Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO);
        Task<BeerDTO> UpdateBeerAsync(int id, string newName,double newABV);
        Task DeleteBeerAsync(int id);
        Task RateAsync(int beerId, int userId, double ratingValue);
        Task<ReviewDTO> ReviewAsync(ReviewDTO reviewDTO);
        Task<ICollection<BeerDTO>> SortByNameAsync();
        Task<ICollection<BeerDTO>> SortByRatingAsync();
        Task<ICollection<BeerDTO>> SortByAbvAsync();
        Task<ICollection<BeerDTO>> FilterByCountryAsync(string country);
        Task<ICollection<BeerDTO>> FilterByStyleAsync(string style);

        
    }
}
