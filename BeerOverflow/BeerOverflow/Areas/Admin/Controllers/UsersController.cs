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
using BeerOverflow.Areas.Admin.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllUsersAsync();
            var result = users.GetViewModels();
            return View(result);
        }

        public async Task<IActionResult> BanUser(int userId)
        {
            await this.userService.BanUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnbanUser(int userId)
        {
            await this.userService.UnbanUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AllFlaggedReviews()
        {
            var reviews = await this.userService.RetreiveAllFlaggedReviewsAsync();
            var result = reviews.GetViewModels();
            return View(result);
        }

        public async Task<IActionResult> UnflagReview(int reviewId)
        {
            await this.userService.ReviewUnflagAsync(reviewId);

            return RedirectToAction(nameof(AllFlaggedReviews));
        }

        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await this.userService.ReviewDeleteAsync(reviewId);

            return RedirectToAction(nameof(AllFlaggedReviews));
        }

    }
}
