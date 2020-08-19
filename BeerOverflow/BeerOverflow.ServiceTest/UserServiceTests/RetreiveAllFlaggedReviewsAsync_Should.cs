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
    public class RetreiveAllFlaggedReviewsAsync_Should
    {
        [TestMethod]

        public async Task RetrieveAllFlaggedReviews()
        {
            var options = TestUtils.GetOptions(nameof(RetrieveAllFlaggedReviews));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Boiko Borisov",
            };
             var beer = new Beer
            {
                Name = "Zagorka",
            };

            var review = new Review
            {
                UserId = 1,
                BeerId = 1,
                isFlagged = true,
            }; 
            var review2 = new Review
            {
                UserId = 1,
                BeerId = 1,
                isFlagged = true,
            };
             var review3 = new Review
            {
                UserId = 1,
                BeerId = 1,
                isFlagged = true,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Reviews.AddAsync(review);
                await arrangeContext.Reviews.AddAsync(review2);
                await arrangeContext.Reviews.AddAsync(review3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.RetreiveAllFlaggedReviewsAsync();
                var result = resultDTOs.Select(x => x.isFlagged).ToList();
                var expected = 3;

                Assert.AreEqual(expected, result.Count);
            }

        }
    }
}
