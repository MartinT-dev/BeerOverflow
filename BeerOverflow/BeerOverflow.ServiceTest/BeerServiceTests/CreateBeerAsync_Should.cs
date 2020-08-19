
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
    public class CreateBeerAsync_Should
    {
        [TestMethod]
        public async Task ThrowNullExc_If_BeerAlreadyExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExc_If_BeerAlreadyExists));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var beer = new Beer
            {
                Name = "Ariana"
            };

            var beerDTO = new BeerDTO
            {
                Name = "Ariana"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Beers.AddAsync(beer);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBeerAsync(beerDTO));
            }
        }

        [TestMethod]
        public async Task ThrowNullExcForBeer_If_BreweryDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExcForBeer_If_BreweryDoesNotExist));
            var mockDateTime = new Mock<IDateTimeProvider>();

            var country = new Country
            {
                Name = "Romania"
            };
            var brewery = new Brewery
            {
                Name = "Kamenitza",
                CountryId = 1
            };
            var style = new Style
            {
                Name = "Pale"
            };

            var beerDTO = new BeerDTO
            {
                Name = "Birichka",
                Country = "Romania",
                Brewery = "Ariana",
                Style = "Pale"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBeerAsync(beerDTO));
            }
        }

        [TestMethod]
        public async Task ThrowNullExcFroBeer_If_StyleDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNullExcFroBeer_If_StyleDoesNotExist));
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

            var beerDTO = new BeerDTO
            {
                Name = "Birichka",
                Country = "Romania",
                Brewery = "Ariana",
                Style = "Dark Lager"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBeerAsync(beerDTO));
            }
        }

        [TestMethod]
        public async Task CreateBeer_When_ParamsAreValid()
        {
            var options = TestUtils.GetOptions(nameof(CreateBeer_When_ParamsAreValid));
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

            var beerDTO = new BeerDTO
            {
                Name = "Birichka",
                Country = "Romania",
                Brewery = "Ariana",
                Style = "Pale"
            };

            using (var arrangeContext = new BeeroverflowContext(options))
            {
                await arrangeContext.Countries.AddAsync(country);
                await arrangeContext.Breweries.AddAsync(brewery);
                await arrangeContext.Styles.AddAsync(style);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new BeeroverflowContext(options))
            {
                var sut = new BeerService(assertContext, mockDateTime.Object);
                var result = await sut.CreateBeerAsync(beerDTO);
                var comparison = assertContext.Beers.Where(x => x.Id == 1).FirstOrDefault();

                Assert.AreEqual("Birichka", comparison.Name);
            }
        }
    }
}

