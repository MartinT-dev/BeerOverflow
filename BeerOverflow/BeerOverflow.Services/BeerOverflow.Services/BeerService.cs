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
    public class BeerService : IBeerService
    {
        private readonly BeeroverflowContext context;
        private readonly IDateTimeProvider dateTimeProvider;

        public BeerService(BeeroverflowContext context,IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO)
        {
            var check = await this.context.Beers
                    .FirstOrDefaultAsync(x => x.Name == beerDTO.Name);

            if (check != null)
            {
                throw new ArgumentNullException("Beer already exist.");
            }

            var brewery = await this.context.Breweries
                .Include(x=>x.Country)
                .Where(x => x.Name == beerDTO.Brewery)
                .FirstOrDefaultAsync();

            if (brewery == null)
            {
                throw new ArgumentNullException("Brewery does not exist.");
            }

            var country = brewery.Country;

            var style = await this.context.Styles
               .Where(x => x.Name == beerDTO.Style)
               .FirstOrDefaultAsync();

            if (style == null)
            {
                throw new ArgumentNullException("Style does not exist.");
            }

            var beer = new Beer
            {
                Id = beerDTO.Id,
                Name = beerDTO.Name,
                Abv = beerDTO.Abv,
                BreweryId = brewery.Id,
                CountryId = country.Id,
                StyleId = style.Id,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Beers.AddAsync(beer);
            await this.context.SaveChangesAsync();
            beerDTO.Id = beer.Id;
            return beerDTO;
        }

        public async Task DeleteBeerAsync(int id)
        {
            try
            {
                var beer = await this.context.Beers
                    .Where(x => x.isDeleted == false)
                    .FirstOrDefaultAsync(x => x.Id == id);

                beer.isDeleted = true;
                beer.CountryId = null;
                beer.Country = null;
                beer.Brewery = null;
                beer.BreweryId = null;
                beer.Style = null;
                beer.StyleId = null;

                beer.DeletedOn = this.dateTimeProvider.GetDateTime();
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Beer does not exist.");
            }
        }

        public async Task<ICollection<BeerDTO>> GetAllBeersAsync()
        {
            var beers = await this.context.Beers
                .Include(b=>b.Style)
                .Include(b=>b.Brewery)
                .Include(b=>b.Country)
                .Include(b=>b.Ratings)
                .ThenInclude(r=>r.User)
                .Include(b=>b.Reviews)
                .ThenInclude(r=>r.User)
                .Where(x => x.isDeleted == false)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            return beers.GetDtos();
        }

        public async Task<BeerDTO> GetBeerAsync(int id)
        {
            var beer = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(u=>u.Likes)
                .ThenInclude(l=>l.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (beer == null)
            {
                throw new ArgumentNullException("Beer does not exist.");
            }

            return beer.GetDto();
        }

        public async Task<BeerDTO> UpdateBeerAsync(int id, string newName,double newABV)
        {
            var beer = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (beer == null)
            {
                throw new ArgumentNullException("Beer does not exist.");
            }

            beer.Name = newName;
            beer.Abv = newABV;
            beer.ModifiedOn = this.dateTimeProvider.GetDateTime();

            await this.context.SaveChangesAsync();
            return beer.GetDto();
        }

        public async Task<ICollection<BeerDTO>> SortByNameAsync()
        {
            var beers = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .OrderBy(x=>x.Name)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            return beers.GetDtos();
        }
        public async Task<ICollection<BeerDTO>> SortByRatingAsync()
        {
            var beers = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            var dict = new SortedDictionary<double, Beer>();
            foreach (var beer in beers)
            {
                double avg = 0;
                if (beer.Ratings.Count() != 0)
                {
                    foreach (var rating in beer.Ratings)
                    {
                        avg += rating.RatingValue;
                    }
                    avg = avg / beer.Ratings.Count();
                }
                dict.Add(avg, beer);
            }

            var result = new List<Beer>();

            foreach (var kvp in dict)
            {
                result.Add(kvp.Value);
            }
            result.Reverse();

            return result.GetDtos();
        }
        public async Task<ICollection<BeerDTO>> SortByAbvAsync()
        {
            var beers = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .OrderBy(x => x.Abv)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            return beers.GetDtos();
        }

        public async Task<ICollection<BeerDTO>> FilterByCountryAsync(string country)
        {
            var beers = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .Where(x => x.Country.Name == country)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            return beers.GetDtos();
        }

        public async Task<ICollection<BeerDTO>> FilterByStyleAsync(string style)
        {
            var beers = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => x.isDeleted == false)
                .Where(x => x.Style.Name == style)
                .ToListAsync();

            if (beers.Any() == false)
            {
                throw new ArgumentNullException("No beers exist.");
            }

            return beers.GetDtos();
        }

        public async Task RateAsync(int beerId, int userId, double ratingValue)
        {
            var user = await this.context.Users
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exist.");
            }

            var beer = await this.context.Beers
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == beerId);

            if (beer == null)
            {
                throw new ArgumentNullException("Beer does not exist.");
            }

            var check = await this.context.Ratings
                .Where(x => x.BeerId == beerId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                check.RatingValue = ratingValue;
            }
            else
            {
                var rating = new Rating
                {
                    UserId = userId,
                    User = user,
                    BeerId = beerId,
                    Beer = beer,
                    RatingValue = ratingValue,
                    CreatedOn = this.dateTimeProvider.GetDateTime(),
                };
                await this.context.Ratings.AddAsync(rating);
            }
            await this.context.SaveChangesAsync();
        }
        public async Task<ReviewDTO> ReviewAsync(ReviewDTO reviewDTO)
        {
            var user = await this.context.Users
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == reviewDTO.UserId);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exist.");
            }

            var beer = await this.context.Beers
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == reviewDTO.BeerId);

            if (beer == null)
            {
                throw new ArgumentNullException("Beer does not exist.");
            }

            var review = new Review
            {
                UserId = reviewDTO.UserId,
                User = user,
                BeerId = reviewDTO.BeerId,
                Beer = beer,
                Title = reviewDTO.Title,
                Text = reviewDTO.Text,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Reviews.AddAsync(review);
            await this.context.SaveChangesAsync();
            return reviewDTO;
        }
    }
}
