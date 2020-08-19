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
    public class AddToDrankListAsync_Should
    {
        [TestMethod]
        public async Task AddToDrankList_If_Successfull()
        {
            var options = TestUtils.GetOptions(nameof(AddToDrankList_If_Successfull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Boiko Borisov",
            };
            var beer = new Beer
            {
                Name = "Zagorka",
            };

            var drankList = new DrankList
            {
                BeerId = 1,
                UserId = 1,
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.DrankLists.AddAsync(drankList);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                await sut.AddToDrankListAsync(1, user, 1);
                var result = assertContext.DrankLists.Where(x => x.BeerId == 1 || x.UserId == 1).FirstOrDefault();

                Assert.AreEqual(user.Id,result.UserId);
                Assert.AreEqual(beer.Id,result.BeerId);
            }
        }
    }
}
