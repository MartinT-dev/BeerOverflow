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
    public class RetreiveWishListAsync_Should
    {
        [TestMethod]
        public async Task ReturnWishList_If_Successfull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnWishList_If_Successfull));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Ariana",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beer = new Beer
            {
                Name = "Zagorka",
                CountryId = 1,
                BreweryId = 1,
                StyleId = 1,
                Abv = 3
            };

            var user = new User
            {
                UserName = "Kolio Mastikata",
                Country = "Bulgaristan"
            };

            var wishList = new WishList
            {
                BeerId = 1,
                UserId = 1,
            };


            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.WishLists.AddAsync(wishList);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new UserService(assertContext, mockDateTime.Object);
                var resultDTO = await sut.RetreiveWishListAsync(1);
                var result = resultDTO.ToArray();

                Assert.AreEqual("Zagorka", result[0].Name);
                Assert.AreEqual(1, result.Count());
            }
        }
    }
}
