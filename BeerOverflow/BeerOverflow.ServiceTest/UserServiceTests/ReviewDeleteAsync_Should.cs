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

namespace BeerOverflow.ServiceTest.UserServiceTests
{
    [TestClass]
    public class ReviewDeleteAsync_Should
    {
        [TestMethod]

        public async Task ReturnTrue_If_Succeds()
        {
            var options = TestUtils.GetOptions(nameof(ReturnTrue_If_Succeds));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var review = new Review
            {
                isDeleted = false,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Reviews.AddAsync(review);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {

                var sut = new UserService(assertContext, mockDateTime.Object);
                await sut.ReviewDeleteAsync(1);
                var result = assertContext.Reviews.Where(x => x.Id == 1).FirstOrDefault();
                Assert.IsTrue(result.isDeleted);
            }

        }

    }
}
