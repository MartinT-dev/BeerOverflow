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

namespace BeerOverflow.ServiceTest.BreweryServiceTests
{

    [TestClass]
    public class DeleteBeerAsync_Should
    {
        [TestMethod]

        public async Task DeleteBeer_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(DeleteBeer_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var beer = new Beer
            {
                Name = "Zagorka"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                await sut.DeleteBeerAsync(1);
                var result = assertContext.Beers.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsTrue(result.isDeleted);
            }

        }

        [TestMethod]
        public async Task FailToDeleteBeer_When_ParamsAreInvalid()
        {
            var options = TestUtils.GetOptions(nameof(FailToDeleteBeer_When_ParamsAreInvalid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.DeleteBeerAsync(1));
            }

        }
    }

}
