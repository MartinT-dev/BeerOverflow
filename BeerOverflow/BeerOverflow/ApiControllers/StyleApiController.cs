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
    [Route("api/styles")]
    [ApiController]
    public class StyleApiController : ControllerBase
    {
        private readonly IStyleService styleService;

        public StyleApiController(IStyleService styleService)
        {
            this.styleService = styleService ?? throw new ArgumentNullException(nameof(styleService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var models = await this.styleService.GetAllStylesAsync();
            var result = models.GetViewModels()
                .Select(x => new { StyleId = x.Id, StyleName = x.Name })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await this.styleService.GetStyleAsync(id);
                var vm = model.GetViewModel();
                var result = new
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Beers = vm.Beers.Select(z => new {
                            Id = z.Id,
                            Name = z.Name,
                            Abv = z.Abv,
                            Brewery = z.Brewery,
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
        public async Task<IActionResult> Post([FromBody] StyleViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var styleDTO = new StyleDTO
            {
                Id = model.Id,
                Name = model.Name,
            };

            var newStyle = await this.styleService.CreateStyleAsync(styleDTO);

            return Created("Post", newStyle);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StyleDTO model)
        {
            if (id == 0 || model == null)
            {
                return BadRequest();
            }

            var style = await this.styleService.UpdateStyleAsync(model.Id, model.Name);

            return Ok(style);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.styleService.DeleteStyleAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
