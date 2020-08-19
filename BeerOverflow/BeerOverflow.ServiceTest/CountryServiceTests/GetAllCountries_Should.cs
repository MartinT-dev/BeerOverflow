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

namespace BeerOverflow.ServiceTest.CountryServiceTests
{
    [TestClass]
    public class GetAllCountriesAsync_Should
    {
        [TestMethod]

        public async Task ReturnAll_If_NotNull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAll_If_NotNull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };
            var country2 = new Country
            {
                Id = 2,
                Name = "Romania"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Countries.AddAsync(country2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.GetAllCountriesAsync();
                var result = resultDTOs.Select(x => x.Name).ToList();

                Assert.AreEqual(2, result.Count());
                Assert.IsTrue(result.Contains("Bulgaria"));
                Assert.IsTrue(result.Contains("Romania"));
            }

        }

        [TestMethod]
        public async Task ThrowNullExc_If_Null()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_Null));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllCountriesAsync());
            }
        }
    }
}
