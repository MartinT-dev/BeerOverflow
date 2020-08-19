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
    public class RemoveFromDrankListAsync_Should
    {
        [TestMethod]
        public async Task RemoveFromDrankList_If_Successfull()
        {
            var options = TestUtils.GetOptions(nameof(RemoveFromDrankList_If_Successfull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Boiko Borisov",
            };
            var beer = new Beer
            {
                Name = "Zagorka",
            };

            var wishList = new DrankList
            {
                BeerId = 1,
                UserId = 1,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.DrankLists.AddAsync(wishList);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                await sut.RemoveFromDrankListAsync(1, user, 1);
                var result = assertContext.DrankLists.Where(x => x.BeerId == 1 || x.UserId == 1).FirstOrDefault();

                Assert.IsNull(result);
            }
        }
    }
}
