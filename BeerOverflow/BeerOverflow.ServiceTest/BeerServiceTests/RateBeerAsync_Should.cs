
using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.ServiceTest.BreweryServiceTests
{
    [TestClass]
    public class RateBeerAsync_Should
    {
        [TestMethod]
        public async Task FailRate_If_UserDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(FailRate_If_UserDoesNotExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 3
            };

            var user = new User
            {
                UserName = "Kolio Mastikata",
                Country = "Bulgaristan"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.RateAsync(1, 2, 4.5));
            }
        }

        [TestMethod]
        public async Task FailRate_If_BeerDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(FailRate_If_BeerDoesNotExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 3
            };

            var user = new User
            {
                UserName = "Kolio Mastikata",
                Country = "Bulgaristan"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.RateAsync(2, 1, 4.5));
            }
        }

        [TestMethod]
        public async Task ChangeRateForBeer_If_RateForSameUserAlreadyExist()
        {
            var options = TestUtils.GetOptions(nameof(ChangeRateForBeer_If_RateForSameUserAlreadyExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 3
            };

            var user = new User
            {
                UserName = "Kolio Mastikata",
                Country = "Bulgaristan"
            };

            var rating = new Rating
            {
                UserId = 1,
                BeerId = 1,
                RatingValue = 3.5
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Ratings.AddAsync(rating);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                await sut.RateAsync(1, 1, 4.5);
                var check = await assertContext.Ratings.Where(x => x.BeerId == 1).ToListAsync();

                Assert.AreEqual(4.5, check[0].RatingValue);
                Assert.AreEqual(1, check.Count());
            }
        }

        [TestMethod]
        public async Task RateBeer_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(RateBeer_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 3
            };

            var user = new User
            {
                UserName = "Kolio Mastikata",
                Country = "Bulgaristan"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                await sut.RateAsync(1,1,4.5);
                var check = await assertContext.Ratings.Where(x => x.BeerId == 1).ToListAsync();

                Assert.AreEqual(4.5, check[0].RatingValue);
                Assert.AreEqual(1, check.Count());
            }
        }
    }
}

