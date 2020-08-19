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
    public class GetBeerAsync_Should
    {
        [TestMethod]
        public async Task Return_OneBeer_When_Exist()
        {
            var options = TestUtils.GetOptions(nameof(Return_OneBeer_When_Exist));
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

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var resultDTO = await sut.GetBeerAsync(1);

                Assert.AreEqual(1, resultDTO.Id);
                Assert.AreEqual("Ariana", resultDTO.Name);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoSingleBreweryExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_When_NoSingleBreweryExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetBeerAsync(1));
            }
        }
    }
}
