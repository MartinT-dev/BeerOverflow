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
    public class UpdateBreweryAsync_Should
    {
        [TestMethod]

        public async Task Update_If_BreweryExist()
        {
            var options = TestUtils.GetOptions(nameof(Update_If_BreweryExist));
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

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);
                var result = await sut.UpdateBreweryAsync(1, "Kamenitza");

                Assert.AreEqual("Kamenitza", result.Name);
            }
        }

        [TestMethod]
        public async Task Update_ThrowException_If_NoBreweryExist()
        {
            var options = TestUtils.GetOptions(nameof(Update_ThrowException_If_NoBreweryExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateBreweryAsync(1, "Asd"));
            }
        }
    }
}
