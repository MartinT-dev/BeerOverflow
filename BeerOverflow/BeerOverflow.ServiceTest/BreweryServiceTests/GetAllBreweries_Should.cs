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
    public class GetAllBreweriesAsync_Should
    {
        [TestMethod]
        public async Task Return_When_Exist()
        {
            var options = TestUtils.GetOptions(nameof(Return_When_Exist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Bulgaria"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId =1
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
                var resultDTOs = await sut.GetAllBreweriesAsync();
                var result= resultDTOs.Select(x => x.Name).ToList();

                Assert.IsTrue(result.Contains("Ariana"));
                Assert.IsTrue(result.Contains("Kamenitza"));
                Assert.AreEqual(2, result.Count);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoBreweryExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_When_NoBreweryExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllBreweriesAsync());
            }
        }
    }
}
