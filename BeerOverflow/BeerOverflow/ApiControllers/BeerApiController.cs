using BeerOverflow.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.ApiControllers
{
     [Route("api/beers")]
     [ApiController]
    public class BeerApiController : ControllerBase
    {
        private readonly IBeerService beerService;

        public BeerApiController(IBeerService beerService)
        {
            this.beerService = beerService ?? throw new ArgumentNullException(nameof(beerService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var models = await this.beerService.GetAllBeersAsync();

            var result = models.GetViewModels()
                .Select(x => new { BeerId = x.Id,
                    BeerName = x.Name,
                    Brewery = x.Brewery, 
                    Country = x.Country,
                    ABV = x.Abv,
                    Rating = x.Rating })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await this.beerService.GetBeerAsync(id);

                return Ok(model);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] BeerViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var beerDTO = new BeerDTO
            {
                Id = model.Id,
                Name = model.Name,
                Abv = model.Abv,
                Country=model.Country,
                Brewery=model.Brewery,
                Style=model.Style
            };

            var newBeer = await this.beerService.CreateBeerAsync(beerDTO);

            return Created("Post", newBeer);
        }

        [HttpPost]
        [Route("{id}/rate")]
        public async Task<IActionResult> Post([FromBody] RatingDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            await this.beerService.RateAsync(model.BeerId, model.UserId, model.RatingValue);

            return Created("Post", model);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BeerViewModel model)
        {
            if (id == 0 || model == null)
            {
                return BadRequest();
            }

            var result = await this.beerService.UpdateBeerAsync(model.Id, model.Name,model.Abv);

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.beerService.DeleteBeerAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("sort")]
        public async Task<IActionResult> SortByName()
        {
            var models = await this.beerService.SortByNameAsync();
            var result = models.GetViewModels()
                .Select(x => new { BeerId = x.Id, BeerName = x.Name, Brewery = x.Brewery, Country = x.Country, ABV = x.Abv, Rating = x.Rating })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("sort/rating")]
        public async Task<IActionResult> SortByRating()
        {
            var models = await this.beerService.SortByRatingAsync();
            var result = models.GetViewModels()
                .Select(x => new { BeerId = x.Id, BeerName = x.Name, Brewery = x.Brewery, Country = x.Country, ABV = x.Abv, Rating = x.Rating })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("sort/abv")]
        public async Task<IActionResult> SortByAbv()
        {
            var models = await this.beerService.SortByAbvAsync();
            var result = models.GetViewModels()
                .Select(x => new { BeerId = x.Id, BeerName = x.Name, Brewery = x.Brewery, Country = x.Country, ABV = x.Abv, Rating = x.Rating })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("filter")]
        public async Task <IActionResult> Filter([FromQuery] string country, [FromQuery] string style)
        {
            if (country != null)
            {
                var models = await this.beerService.FilterByCountryAsync(country);
                var result = models.GetViewModels()
                    .Select(x => new { BeerId = x.Id, BeerName = x.Name, Brewery = x.Brewery, Country = x.Country, ABV = x.Abv, Rating = x.Rating })
                    .ToList();

                return Ok(result);
            }
            if (style != null)
            {
                var models = await this.beerService.FilterByStyleAsync(style);
                var result = models.GetViewModels()
                    .Select(x => new { BeerId = x.Id, BeerName = x.Name, Brewery = x.Brewery, Country = x.Country, ABV = x.Abv, Rating = x.Rating })
                    .ToList();

                return Ok(result);
            }
            return NotFound();
        }
    }
}
