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

namespace BeerOverflow.ServiceTest.StyleServiceTests
{
    [TestClass]
    public class GetStyleAsync_Should
    {
        [TestMethod]
        public async Task GetStyle_If_StyleExists()
        {
            var options = TestUtils.GetOptions(nameof(GetStyle_If_StyleExists));
            var mockDateTime = new Mock<IDateTimeProvider>();
            var style = new Style
            {
                Id = 1,
                Name = "Pale"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);
                var result = await sut.GetStyleAsync(1);

                Assert.AreEqual(style.Id, result.Id);
                Assert.AreEqual(style.Name, result.Name);
            }
        }

        [TestMethod]
        public async Task ThrowNullExc_If_NoStyleExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_NoStyleExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetStyleAsync(3));
            }
        }
    }
}
