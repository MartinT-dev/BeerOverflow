using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
using BeerOverflow.Services.DTOs;
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
    public class CreateUserAsync_Should
    {
        [TestMethod]
        public async Task ThrowNullExc_If_UserAlreadyExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_UserAlreadyExists));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var user = new User
            {
                UserName = "Tony Montana"
            };

            var userDTO = new UserDTO
            {
                Username = "Tony Montana"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateUserAsync(userDTO));
            }
        }

        [TestMethod]
        public async Task CreateUser_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(CreateUser_When_ParamsAreValid));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var userDTO = new UserDTO
            {
                Username = "BoikoBorisov2",
                Country = "Bulgaria",
          
            };


            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                try
                {
                var result = await sut.CreateUserAsync(userDTO);
                     
                }
                catch (Exception)
                {

                    throw;
                }
                var comparison = assertContext.Users.Where(x => x.Id == 1).FirstOrDefault();

                Assert.AreEqual("BoikoBorisov2", comparison.UserName);
                Assert.AreEqual("Bulgaria", comparison.Country);
            }
        }
    }


}
