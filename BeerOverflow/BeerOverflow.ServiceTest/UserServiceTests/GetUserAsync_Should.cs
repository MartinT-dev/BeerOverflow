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
   public class GetUserAsync_Should
    {
        [TestMethod]
        public async Task Return_OneUser_When_Exist()
        {
            var options = TestUtils.GetOptions(nameof(Return_OneUser_When_Exist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Future",
                Country = "America",
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                var resultDTO = await sut.GetUserAsync(1);

                Assert.AreEqual(1, resultDTO.Id);
                Assert.AreEqual("Future", resultDTO.Username);
                Assert.AreEqual("America", resultDTO.Country);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoSingleUserExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_When_NoSingleUserExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetUserAsync(1));
            }
        }
    }
}
