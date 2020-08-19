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

namespace BeerOverflow.ServiceTest.UserServiceTests
{
    [TestClass]
    public class UpdateUserAsync_Should
    {
        [TestMethod]

        public async Task UpdateUser_If_BeerExist()
        {
            var options = TestUtils.GetOptions(nameof(UpdateUser_If_BeerExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Don Q",
                Country = "Canada"
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
                var result = await sut.UpdateUserAsync(1, "Dax");

                Assert.AreEqual("Dax", result.Username);
            }
        }

        [TestMethod]
        public async Task UpdateUserFail_If_NoUserExist()
        {
            var options = TestUtils.GetOptions(nameof(UpdateUserFail_If_NoUserExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateUserAsync(1, "Asd"));
            }
        }
    }
}
