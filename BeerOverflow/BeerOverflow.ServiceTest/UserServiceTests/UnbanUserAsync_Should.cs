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
    public class UnBanUserAsync_Should
    {
        [TestMethod]

        public async Task UnBanUser_If_Successfull()
        {
            var options = TestUtils.GetOptions(nameof(UnBanUser_If_Successfull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                isBanned = true,
                LockoutEnabled = true
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {

                var sut = new UserService(assertContext, mockDateTime.Object);
                await sut.UnbanUserAsync(1);
                var result = assertContext.Users.Where(x => x.Id == 1).FirstOrDefault();
                Assert.IsFalse(result.isBanned);
                
            }
        }
    }
}
