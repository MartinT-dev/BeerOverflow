
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
    [Route("api/countries")]
    [ApiController]
    public class CountryApiController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryApiController(ICountryService countryService)
        {
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var models = await this.countryService.GetAllCountriesAsync();
            var result = models.GetViewModels()
                .Select(x=> new { CountryId = x.Id, Name = x.Name })
                .ToList();

            return Ok(result);            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            try
            {
                var model = await this.countryService.GetCountryAsync(id);
                var vm = model.GetViewModel();
                var result = new  
                { 
                    Id = vm.Id, 
                    Name = vm.Name, 
                    Breweries = vm.Breweries.Select(y=> new { 
                        Id = y.Id, 
                        Name = y.Name, 
                        Beers = y.Beers.Select(z=> new { 
                            Id = z.Id,
                            Name = z.Name,
                            Abv = z.Abv,
                            Style = z.Style
                        })
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
        public async Task<IActionResult> Post([FromBody] CountryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var countryDTO = new CountryDTO
            {
                Id = model.Id,
                Name = model.Name,
            };

            var newCountry = await this.countryService.CreateCountryAsync(countryDTO);

            return Created("Post", newCountry);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id , [FromBody] CountryViewModel model)
        {
            if (id == 0 || model == null)
            {
                return BadRequest();
            }

            var country = await this.countryService.UpdateCountryAsync(model.Id, model.Name);

            return Ok(country);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.countryService.DeleteCountryAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
