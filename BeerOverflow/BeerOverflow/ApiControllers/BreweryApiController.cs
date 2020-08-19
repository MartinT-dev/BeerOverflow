
using BeerOverflow.Mappers;
using BeerOverflow.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.ApiControllers
{
    [Route("api/breweries")]
    [ApiController]
    public class BreweryApiController : ControllerBase
    {
        private readonly IBreweryService breweryService;

        public BreweryApiController(IBreweryService breweryService)
        {
            this.breweryService = breweryService ?? throw new ArgumentNullException(nameof(breweryService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var models = await this.breweryService.GetAllBreweriesAsync();
            var result = models.GetViewModels()
                .Select(x => new { BreweryId = x.Id, Name = x.Name })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await this.breweryService.GetBreweryAsync(id);
                var vm = model.GetViewModel();

                var result = new
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Beers = vm.Beers.Select(z => new {
                        Id = z.Id,
                        Name = z.Name,
                        Abv = z.Abv,
                        Style = z.Style,
                        Country = z.Country
                    })
                };

                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] BreweryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var breweryDTO = new BreweryDTO
            {
                Id = model.Id,
                Name = model.Name,
                Country = model.Country
            };
            var newBrewery = await this.breweryService.CreateBreweryAsync(breweryDTO);

            return Created("Post", newBrewery);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BreweryDTO model)
        {
            if (id == 0 || model == null)
            {
                return BadRequest();
            }

            var brewery = await this.breweryService.UpdateBreweryAsync(model.Id, model.Name);

            return Ok(brewery);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.breweryService.DeleteBreweryAsync(id);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
