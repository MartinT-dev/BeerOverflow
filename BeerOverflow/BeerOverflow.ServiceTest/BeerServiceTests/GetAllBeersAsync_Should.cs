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
    public class GetAllBeersAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllBeers_When_AtLeastOneExist()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAllBeers_When_AtLeastOneExist));
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

            var beer = new Beer
            {
                Name = "Ariana",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,

            };

            var beer2 = new Beer
            {
                Name = "Kamenitza",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Beers.AddAsync(beer2);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.GetAllBeersAsync();
                var result = resultDTOs.Select(x => x.Name).ToList();

                Assert.IsTrue(result.Contains("Ariana"));
                Assert.IsTrue(result.Contains("Kamenitza"));
                Assert.AreEqual(2, result.Count);
            }
        }
        [TestMethod]
        public async Task FailAllBeers_WhenNoBeerExist()
        {
            var options = TestUtils.GetOptions(nameof(FailAllBeers_WhenNoBeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllBeersAsync());
            }
        }
    }
}
