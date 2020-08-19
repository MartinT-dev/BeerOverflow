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
using BeerOverflow.Mappers;
using X.PagedList;
using BeerOverflow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeerService beerService;
        private readonly IBreweryService breweryService;
        private readonly ICountryService countryService;
        private readonly IStyleService styleService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public BeersController(IBeerService beerService, IBreweryService breweryService, ICountryService countryService, IStyleService styleService, UserManager<User> userManager, IUserService userService)
        {
            this.beerService = beerService ?? throw new ArgumentNullException(nameof(beerService));
            this.breweryService = breweryService ?? throw new ArgumentNullException(nameof(breweryService));
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            this.styleService = styleService ?? throw new ArgumentNullException(nameof(styleService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: Beers
        public async Task<IActionResult> Index(int? page, string searchString)
        {
            var beerDtos = await this.beerService.GetAllBeersAsync();
            var beers = beerDtos.GetViewModels().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                beers = beers.Where(s => s.Name.Contains(searchString)
                                       || s.Country.Contains(searchString) || s.Style.Contains(searchString) || 
                                       s.Rating.ToString().Contains(searchString));
            }

            return View(await beers.ToListAsync());

        }

         


        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var beer = await this.beerService.GetBeerAsync(id);
            var result = beer.GetViewModel();
            if (beer == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Beers/Create
        public async Task<IActionResult> Create()
        {
            var beer = new BeerViewModel();
            var breweries = await this.breweryService.GetAllBreweriesAsync();
            var styles = await this.styleService.GetAllStylesAsync();

            beer.Breweries = breweries.Select(b => new SelectListItem(b.Name, b.Name)).ToList();
            beer.Styles = styles.Select(b => new SelectListItem(b.Name, b.Name)).ToList();

            return View(beer);
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel beer)
        {
            if (ModelState.IsValid)
            {
                var dto = beer.GetDTO();
                await this.beerService.CreateBeerAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }
        [Authorize(Roles = "Admin")]
        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var beer = await this.beerService.GetBeerAsync(id);
            var result = beer.GetViewModel();
            if (beer == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BeerViewModel beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.beerService.UpdateBeerAsync(id, beer.Name, beer.Abv);
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }
        [Authorize(Roles = "Admin")]
        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var beer = await this.beerService.GetBeerAsync(id);
            var result = beer.GetViewModel();
            if (beer == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.beerService.DeleteBeerAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Review(int beerId)
        {
            var review = new ReviewViewModel();

            review.BeerId = beerId;

            return View(review);
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                review.UserId = user.Id;
                review.User = user.UserName;

                var dto = review.GetDTO();
                await this.beerService.ReviewAsync(dto);

                return RedirectToAction($"Details", "Beers", new { ID = dto.BeerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                var reviewId = review.Id;
                var user = await userManager.GetUserAsync(User);

                await this.userService.ReviewLikeAsync(reviewId, user.Id, user);

                return RedirectToAction($"Details", "Beers", new { ID = review.BeerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Dislike(ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                var reviewId = review.Id;
                var user = await userManager.GetUserAsync(User);

                await this.userService.ReviewDislikeAsync(reviewId, user.Id, user);

                return RedirectToAction($"Details", "Beers", new { ID = review.BeerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Flag(ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                var reviewId = review.Id;
                await this.userService.ReviewFlagAsync(reviewId);

                return RedirectToAction($"Details", "Beers", new { ID = review.BeerId });
            }
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Rate(int id)
        {
            var vm = new RatingViewModel();

            vm.BeerId = id;

            return View(vm);
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(RatingViewModel rating)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                await this.beerService.RateAsync(rating.BeerId, user.Id, rating.RatingValue);

                return RedirectToAction($"Details", "Beers", new { ID = rating.BeerId });
            }
            return View();
        }
    }
}
