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
    public class GetAllUsersAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllUsers_When_AtLeastOneExist()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAllUsers_When_AtLeastOneExist));
            var mockDateTime = new Mock<IDateTimeProvider>();


            var user = new User
            {
                UserName = "Joyner Lucas",
                Country = "Ameirca",


            };

            var user2 = new User
            {
                UserName = "Eminem",
                Country = "America",
    
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
    
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Users.AddAsync(user2);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                var resultDTOs = await sut.GetAllUsersAsync();
                var resultName = resultDTOs.Select(x => x.Username).ToList();
                var resultCountry = resultDTOs.Select(x => x.Country).ToList();

                Assert.IsTrue(resultName.Contains("Joyner Lucas"));
                Assert.IsTrue(resultCountry.Contains("America"));
                Assert.IsTrue(resultName.Contains("Eminem"));
                Assert.IsTrue(resultCountry.Contains("America"));
                Assert.AreEqual(2, resultName.Count);
                Assert.AreEqual(2, resultCountry.Count);
            }
        }
        [TestMethod]
        public async Task FailAllUsers_WhenNoUserExist()
        {
            var options = TestUtils.GetOptions(nameof(FailAllUsers_WhenNoUserExist));
            var mockDateTime = new Mock<IDateTimeProvider>(); 

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllUsersAsync());
            }
        }
    }
}
