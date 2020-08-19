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
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services
{
    public class CountryService : ICountryService
    {
        private readonly BeeroverflowContext context;
        private readonly IDateTimeProvider dateTimeProvider;

        public CountryService(BeeroverflowContext context, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO)
        {
            var check = await this.context.Countries
                    .FirstOrDefaultAsync(x => x.Name == countryDTO.Name);
            if (check != null)
            {
                throw new ArgumentNullException("Country already existing.");
            }
            var country = new Country
            {
                Name = countryDTO.Name,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };
            
            await this.context.Countries.AddAsync(country);
            await this.context.SaveChangesAsync();
            countryDTO.Id = country.Id;
            return countryDTO;
        }

        public async Task DeleteCountryAsync(int id)
        {
            try
            {
                var country = await this.context.Countries
                    .Where(x => x.isDeleted == false)
                    .FirstOrDefaultAsync(x => x.Id == id);

                country.isDeleted = true;
                country.DeletedOn = this.dateTimeProvider.GetDateTime();
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Country not existing.");
            }
        }

        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            var countries = await this.context.Countries
             .Where(x => x.isDeleted == false)
             .ToListAsync();

            if (countries.Any() == false)
            {
                throw new ArgumentNullException("No countries exist.");
            }

            return countries.GetDtos();
        }

        public async Task<CountryDTO> GetCountryAsync(int id)
        {
            var country =  await this.context.Countries
                .Include(c => c.Breweries)
                .ThenInclude(b => b.Beers)
                .ThenInclude(b => b.Style)
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException("Country does not exist.");
            }
  
            return country.GetDto();
        }

        public async Task<CountryDTO> UpdateCountryAsync(int id, string newName)
        {
            var country = await this.context.Countries
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException("Country does not exist.");
            }

            country.Name = newName;
            country.ModifiedOn = this.dateTimeProvider.GetDateTime();
            await this.context.SaveChangesAsync();
            
            return country.GetDto();
        }
    }
}
