
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

namespace BeerOverflow.ServiceTest.StyleServiceTests
{
    [TestClass]
    public class CreateStyleAsync_Should
    {
        [TestMethod]

        public async Task CreateNewStyle_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(CreateNewStyle_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var styleDTO = new StyleDTO
            {
                Id = 1,
                Name = "Pale"
            };

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new StyleService(assertContext, mockDateTime.Object);
                var styles = await sut.CreateStyleAsync(styleDTO);

                Assert.AreEqual(1, styles.Id);
                Assert.AreEqual("Pale", styles.Name);
            }
        }

        [TestMethod]
        public async Task ThrowExc_If_StyleAlreadyExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExc_If_StyleAlreadyExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var style = new Style
            {
                Name = "Pale"
            };

            var styleDTO = new StyleDTO
            {
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

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateStyleAsync(styleDTO));
            }
        }

    }
}
