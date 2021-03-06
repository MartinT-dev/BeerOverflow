﻿
using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.ServiceTest.BreweryServiceTests
{
    [TestClass]
    public class ReviewBeerAsync_Should
    {
        [TestMethod]
        public async Task FailReview_If_UserDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(FailReview_If_UserDoesNotExist));
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

            var review = new ReviewDTO
            {
                UserId = 2,
                BeerId = 1,
                Text = "text",
                Title = "title"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.ReviewAsync(review));
            }
        }

        [TestMethod]
        public async Task FailReview_If_BeerDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(FailReview_If_BeerDoesNotExist));
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

            var review = new ReviewDTO
            {
                UserId = 1,
                BeerId = 2,
                Text = "text",
                Title = "title"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.ReviewAsync(review));
            }
        }

        [TestMethod]
        public async Task ReviewBeer_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(ReviewBeer_When_ParamsAreValid));
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

            var review = new ReviewDTO
            {
                UserId = 1,
                BeerId = 1,
                Text = "text",
                Title = "title"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var result = await sut.ReviewAsync(review);
                var check = await assertContext.Reviews.Where(x => x.BeerId == 1).ToListAsync();

                Assert.AreEqual("text", check[0].Text);
                Assert.AreEqual("title", check[0].Title);
                Assert.AreEqual(1, check.Count());
            }
        }
    }
}

