using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Providers.Contracts;
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
    public class SortByRatingAsync_Should
    {
        [TestMethod]
        public async Task SortBeersByRating_When_AtLeastOneExist()
        {
            var options = TestUtils.GetOptions(nameof(SortBeersByRating_When_AtLeastOneExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Kamenitza",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var user = new User
            {
                UserName = "User",
                Country = "Mexico"
                
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1  
            };

            var beer2 = new Beer
            {
                Name = "Kamenitza",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1
            };

            var beer3 = new Beer
            {
                Name = "Ariana",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1
            };

            var rating1 = new Rating
            {
                UserId = 1,
                BeerId = 1,
                RatingValue = 4.4
            };

            var rating2 = new Rating
            {
                UserId = 1,
                BeerId = 2,
                RatingValue = 2.5
            };

            var rating3 = new Rating
            {
                UserId = 1,
                BeerId = 3,
                RatingValue = 3.5
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Beers.AddAsync(beer2);
                await arrangeContext.Beers.AddAsync(beer3);
                await arrangeContext.Ratings.AddAsync(rating1);
                await arrangeContext.Ratings.AddAsync(rating2);
                await arrangeContext.Ratings.AddAsync(rating3);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.SortByRatingAsync();
                var result = resultDTOs.ToArray();

                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].Name, "Zagorka");
                Assert.AreEqual(result[1].Id, 3);
                Assert.AreEqual(result[1].Name, "Ariana");
                Assert.AreEqual(result[2].Id, 2);
                Assert.AreEqual(result[2].Name, "Kamenitza");
                Assert.AreEqual(3, resultDTOs.Count);
            }
        }
        [TestMethod]
        public async Task FailSortBeersByRating_WhenNoBeerExist()
        {
            var options = TestUtils.GetOptions(nameof(FailSortBeersByRating_WhenNoBeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.SortByRatingAsync());
            }
        }
    }
}
