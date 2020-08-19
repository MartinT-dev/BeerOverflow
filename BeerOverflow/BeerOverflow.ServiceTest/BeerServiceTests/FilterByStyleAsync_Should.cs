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
    public class FilterByStyleAsync_Should
    {
        [TestMethod]
        public async Task FilterByStyle_When_AtLeastOneExist()
        {
            var options = TestUtils.GetOptions(nameof(FilterByStyle_When_AtLeastOneExist));
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

            var style2 = new Style
            {
                Name = "Dark Lager"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 2,
                Abv = 3
            };

            var beer2 = new Beer
            {
                Name = "Kamenitza",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 2
            };

            var beer3 = new Beer
            {
                Name = "Ariana",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 1
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Styles.AddAsync(style2);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Beers.AddAsync(beer2);
                await arrangeContext.Beers.AddAsync(beer3);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.FilterByStyleAsync("Dark Lager");
                var result = resultDTOs.ToArray();

                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].Name, "Zagorka");
                Assert.AreEqual(1, resultDTOs.Count);
            }
        }
        [TestMethod]
        public async Task FailFilterByStyle_WhenNoBeerExist()
        {
            var options = TestUtils.GetOptions(nameof(FailFilterByStyle_WhenNoBeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.FilterByStyleAsync("Romania"));
            }
        }
    }
}
