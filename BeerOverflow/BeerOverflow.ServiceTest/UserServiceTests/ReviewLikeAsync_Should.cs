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
    public class ReviewLikeAsync_Should
    {
        [TestMethod]
        public async Task LikeTheReview_If_Successfull()
        {
            var options = TestUtils.GetOptions(nameof(LikeTheReview_If_Successfull));
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
            };

            var like = new Like
            {
                ReviewId = 1,
                UserId = 1,
                isLiked = false,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Reviews.AddAsync(review);
                await arrangeContext.Likes.AddAsync(like);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                await sut.ReviewLikeAsync(1,1,user);
                var result = assertContext.Likes.Where(x => x.ReviewId == 1 || x.UserId == 1).FirstOrDefault();

                Assert.IsTrue(result.isLiked);
            }
        }
    }
}
