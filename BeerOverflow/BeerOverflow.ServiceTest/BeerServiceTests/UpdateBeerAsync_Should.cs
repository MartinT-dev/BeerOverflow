using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
using BeerOverflow.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.ServiceTest.BreweryServiceTests
{
    [TestClass]
    public class UpdateBeerAsync_Should
    {
        [TestMethod]

        public async Task UpdateBeer_If_BeerExist()
        {
            var options = TestUtils.GetOptions(nameof(UpdateBeer_If_BeerExist));
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
                Abv = 4,
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
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var result = await sut.UpdateBeerAsync(1, "Kamenitza", 5.0);

                Assert.AreEqual("Kamenitza", result.Name);
                Assert.AreEqual(5.0, result.Abv);
            }
        }

        [TestMethod]
        public async Task UpdateBeerFail_If_NoBeerExist()
        {
            var options = TestUtils.GetOptions(nameof(UpdateBeerFail_If_NoBeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateBeerAsync(1, "Asd", 5.0));
            }
        }
    }
}
