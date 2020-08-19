using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using BeerOverflow.Models;
using BeerOverflow.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Authorize(Roles = "Admin, User")]
        // GET: Users/Details/5
        public async Task<IActionResult> MyPage()
        {
            var user = await userManager.GetUserAsync(User);

            var vm = new WishListViewModel();
            vm.Username = user.UserName;
            vm.Country = user.Country;
            var dtosWish = await this.userService.RetreiveWishListAsync(user.Id);
            vm.WishLists = dtosWish.GetViewModels();
            var dtosDrank = await this.userService.RetreiveDrankListAsync(user.Id);
            vm.DrankLists = dtosDrank.GetViewModels();

            return View(vm);
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddToWishList(int beerId)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                await this.userService.AddToWishListAsync(user.Id, user, beerId);

                return RedirectToAction($"Details", "Beers", new { ID = beerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> RemoveFromWishList(int beerId)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                await this.userService.RemoveFromWishListAsync(user.Id, user, beerId);

                return RedirectToAction("MyPage");
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddToDrankList(int beerId)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                await this.userService.AddToDrankListAsync(user.Id, user, beerId);

                return RedirectToAction($"Details", "Beers", new { ID = beerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> RemoveFromDrankList(int beerId)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                await this.userService.RemoveFromDrankListAsync(user.Id, user, beerId);

                return RedirectToAction("MyPage");
            }
            return View();
        }

    }
}
