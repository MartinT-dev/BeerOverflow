
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
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.ServiceTest.CountryServiceTests
{
    [TestClass]
    public class CreateCountryAsync_Should
    {
        [TestMethod]

        public async Task CreateCountry_If_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(CreateCountry_If_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var countryDTO = new CountryDTO
            {
                Name = "Bulgaria"
            };

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);
                var result = await sut.CreateCountryAsync(countryDTO);

                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Bulgaria", result.Name);
            }
        }

        [TestMethod]
        public async Task CreateCountry_Fail_IfAlreadyExists()
        {
            var options = TestUtils.GetOptions(nameof(CreateCountry_Fail_IfAlreadyExists));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Bulgaria"
            };

            var countryDTO = new CountryDTO
            {
                Name = "Bulgaria"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCountryAsync(countryDTO));
            }
        }
    }
}
