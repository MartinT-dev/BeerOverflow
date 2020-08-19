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
    public class GetBreweryAsync_Should
    {
        [TestMethod]
        public async Task Return_OneBrewery_When_Exist()
        {
            var options = TestUtils.GetOptions(nameof(Return_OneBrewery_When_Exist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Bulgaria"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };

            var brewery2 = new Brewery
            {
                Name = "Kamenitza",
                CountryId = 1
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Breweries.AddAsync(brewery2);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);
                var resultDTO = await sut.GetBreweryAsync(2);

                Assert.AreEqual(2, resultDTO.Id);
                Assert.AreEqual("Kamenitza", resultDTO.Name);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoSingleBreweryExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_When_NoSingleBreweryExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetBreweryAsync(1));
            }
        }
    }
}
