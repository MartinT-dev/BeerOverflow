
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
    public class CreateBreweryAsync_Should
    {
        [TestMethod]
        public async Task ThrowNullExc_If_BreweryAlreadyExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_BreweryAlreadyExists));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var brewery = new Brewery
            {
                Name = "Ariana"
            };

            var breweryDTO = new BreweryDTO
            {
                Name = "Ariana"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBreweryAsync(breweryDTO));
            }
        }

        [TestMethod]
        public async Task ThrowNullExc_If_CountryDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_CountryDoesNotExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var breweryDTO = new BreweryDTO
            {
                Name = "Ariana",
                Country = "Romania"                
            };

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBreweryAsync(breweryDTO));
            }
        }

        [TestMethod]
        public async Task Create_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(Create_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };

            var breweryDTO = new BreweryDTO
            {
                Name = "Ariana",
                Country = "Romania"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BreweryService(assertContext, mockDateTime.Object);
                var result = await sut.CreateBreweryAsync(breweryDTO);
                var comparison = assertContext.Breweries.Where(x => x.Id == 1).FirstOrDefault();

                Assert.AreEqual("Ariana",comparison.Name);
            }
        }
    }
}
