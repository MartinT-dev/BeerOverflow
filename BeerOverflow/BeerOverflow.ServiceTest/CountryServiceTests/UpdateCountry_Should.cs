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

namespace BeerOverflow.ServiceTest.CountryServiceTests
{
    [TestClass]
    public class UpdateCountryAsync_Should
    {
        [TestMethod]

        public async Task Succeed_If_NotNull()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_If_NotNull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
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
                var result = await sut.UpdateCountryAsync(1, "Romania");

                Assert.AreEqual("Romania" ,result.Name);
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

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateCountryAsync(1, "Asd"));
            }
        }
    }
}
