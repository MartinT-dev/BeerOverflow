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
    public class UpdateStyleAsync_Should
    {
        [TestMethod]

        public async Task UpdateCountry_If_ItExists()
        {
            var options = TestUtils.GetOptions(nameof(UpdateCountry_If_ItExists));
            var mockDateTime = new Mock<IDateTimeProvider>();
            var style = new Style
            {
                Name = "Pale"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);
                var result = await sut.UpdateStyleAsync(1, "Dark Lagers");

                Assert.AreEqual("Dark Lagers", result.Name);
            }
        }

        [TestMethod]
        public async Task ThrowNullExcForUpdate_If_StyleDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExcForUpdate_If_StyleDoesNotExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateStyleAsync(1, "Asd"));
            }
        }
    }
}
