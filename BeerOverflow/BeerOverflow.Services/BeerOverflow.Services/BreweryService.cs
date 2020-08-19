using BeerOverflow.Data;
using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOmappers;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly BeeroverflowContext context;
        private readonly IDateTimeProvider dateTimeProvider;
        public BreweryService(BeeroverflowContext context, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }
        public async Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO)
        {
            var check = await this.context.Breweries
                    .FirstOrDefaultAsync(x => x.Name == breweryDTO.Name);

            if (check != null)
            {
                throw new ArgumentNullException("Brewery already existing");
            }

            var country = await this.context.Countries
                .Where(x => x.Name == breweryDTO.Country)
                .Where(x=> x.isDeleted == false)
                .FirstOrDefaultAsync();

            if (country == null)
            {
                throw new ArgumentNullException("Country does not exist.");
            }

            var brewery = new Brewery
            {
                Name = breweryDTO.Name,
                CountryId = country.Id,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Breweries.AddAsync(brewery);
            await this.context.SaveChangesAsync();
            breweryDTO.Id = brewery.Id;
            return breweryDTO;
        }

        public async Task DeleteBreweryAsync(int id)
        {
            try
            {
                var brewery = await this.context.Breweries
                    .Where(brewery => brewery.isDeleted == false)
                    .FirstOrDefaultAsync(user => user.Id == id);

                brewery.isDeleted = true;
                brewery.CountryId = null;
                brewery.Country = null;
                brewery.DeletedOn = this.dateTimeProvider.GetDateTime();
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Brewery does not exist.");
            }
        }

        public async Task<ICollection<BreweryDTO>> GetAllBreweriesAsync()
        {
            var breweries = await this.context.Breweries
                .Include(b => b.Beers)
                .ThenInclude(b => b.Style)
                .Include(b => b.Country)
                .Where(brewery => brewery.isDeleted == false)
                .ToListAsync();

            if (breweries.Any() == false)
            {
                throw new ArgumentNullException("No breweries exist.");
            }

            return breweries.GetDtos();
        }

        public async Task<BreweryDTO> GetBreweryAsync(int id)
        {
            var brewery = await this.context.Breweries
                .Include(b => b.Beers)
                .ThenInclude(b => b.Style)
                .Include(b => b.Country)
                .Where(brewery => brewery.isDeleted == false)
                .FirstOrDefaultAsync(brewery => brewery.Id == id);

            if (brewery == null)
            {
                throw new ArgumentNullException("Brewery does not exist.");
            }
            
            return brewery.GetDto();
        }

        public async Task<BreweryDTO> UpdateBreweryAsync(int id, string newName)
        {
            var brewery = await this.context.Breweries
                .Include(b => b.Beers)
                .ThenInclude(b => b.Style)
                .Include(b => b.Country)
                .Where(brewery => brewery.isDeleted == false)
                .FirstOrDefaultAsync(brewery => brewery.Id == id);

            if (brewery == null)
            {
                throw new ArgumentNullException("Brewery does not exist.");
            }

            brewery.Name = newName;

            brewery.ModifiedOn = this.dateTimeProvider.GetDateTime();
            await this.context.SaveChangesAsync();
            return brewery.GetDto();
        }
    }
}