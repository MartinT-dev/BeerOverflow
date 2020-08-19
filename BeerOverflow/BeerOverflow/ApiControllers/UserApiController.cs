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
    [Route("api/users")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;

        public UserApiController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var models = await this.userService.GetAllUsersAsync();
            var result = models.GetViewModels()
                .Select(x => new { UserId = x.Id, Username = x.Username, Country = x.Country })
                .ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await this.userService.GetUserAsync(id);
                var result = model.GetViewModel();

                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public  async Task<IActionResult> Post([FromBody] UserViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var userDTO = new UserDTO
            {
                Id = model.Id,
                Username = model.Username,
                Country = model.Country,
            };

            var newUser = await this.userService.CreateUserAsync(userDTO);

            return Created("Post", newUser);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserViewModel model)
        {
            if (id == 0 || model == null)
            {
                return BadRequest();
            }

            var artist = await this.userService.UpdateUserAsync(model.Id, model.Username);

            return Ok(artist);
        }
    } 
    
}
