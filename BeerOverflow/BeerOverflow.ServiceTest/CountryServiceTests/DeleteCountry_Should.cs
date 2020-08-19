using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
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
    public class DeleteCountryAsync_Should
    {
        [TestMethod]

        public async Task ReturnTrue_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(ReturnTrue_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();
            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);
                await sut.DeleteCountryAsync(1);
                var result = assertContext.Countries.Where(x => x.Id == 1).FirstOrDefault();
                Assert.IsTrue(result.isDeleted);
            }

        }

        [TestMethod]
        public async Task ReturnFalse_When_ParamsAreInValid()
        {
            var options = TestUtils.GetOptions(nameof(ReturnFalse_When_ParamsAreInValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new CountryService(assertContext, mockDateTime.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.DeleteCountryAsync(1));
            }

        }
    }
}
