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

namespace BeerOverflow.ServiceTest.StyleServiceTests
{
    [TestClass]
    public class GetAllStylesAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllStyles_If_Exist()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAllStyles_If_Exist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var style = new Style
            {
                Id = 1,
                Name = "Pale"
            };
            var style2 = new Style
            {
                Id = 2,
                Name = "Dark Lagers"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Styles.AddAsync(style2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.GetAllStylesAsync();
                var result = resultDTOs.Select(x => x.Name).ToList();

                Assert.AreEqual(2, result.Count());
                Assert.IsTrue(result.Contains("Pale"));
                Assert.IsTrue(result.Contains("Dark Lagers"));
            }
        }
        [TestMethod]
        public async Task ThrowNullExc_If_NoStylesExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_NoStylesExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllStylesAsync());
            }
        }

    }

}
