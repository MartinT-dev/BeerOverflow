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
    public class FilterByCountryAsync_Should
    {
        [TestMethod]
        public async Task FilterByCountry_When_AtLeastOneExist()
        {
            var options = TestUtils.GetOptions(nameof(FilterByCountry_When_AtLeastOneExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };

            var country2 = new Country
            {
                Name = "Bulgaria"
            };

            var brewery = new Brewery
            {
                Name = "Kamenitza",
                CountryId = 1
            };

            var brewery2 = new Brewery
            {
                Name = "Ariana",
                CountryId = 2
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

            var beer2 = new Beer
            {
                Name = "Kamenitza",
                CountryId = 2,
                BreweryId = 2,
                StyleId = 1,
                Abv = 2
            };

            var beer3 = new Beer
            {
                Name = "Ariana",
                CountryId = 2,
                BreweryId = 2,
                StyleId = 1,
                Abv = 1
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Countries.AddAsync(country2);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Breweries.AddAsync(brewery2);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Beers.AddAsync(beer2);
                await arrangeContext.Beers.AddAsync(beer3);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.FilterByCountryAsync("Romania");
                var result = resultDTOs.ToArray();

                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].Name, "Zagorka");
                Assert.AreEqual(1, resultDTOs.Count);
            }
        }
        [TestMethod]
        public async Task FailFilterByCountry_WhenNoBeerExist()
        {
            var options = TestUtils.GetOptions(nameof(FailFilterByCountry_WhenNoBeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.FilterByCountryAsync("Romania"));
            }
        }
    }
}
