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
    public class DeleteUserAsync_Should
    {
        [TestMethod]

        public async Task DeleteUser_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(DeleteUser_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Joyner Q"
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
                await sut.DeleteUserAsync(1);
                var result = assertContext.Users.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsTrue(result.isDeleted);
            }

        }

        [TestMethod]
        public async Task FailToDeleteUser_When_ParamsAreInvalid()
        {
            var options = TestUtils.GetOptions(nameof(FailToDeleteUser_When_ParamsAreInvalid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.DeleteUserAsync(1));
            }

        }
    }
}
