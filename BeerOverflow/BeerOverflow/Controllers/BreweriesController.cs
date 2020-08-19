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
using BeerOverflow.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly IBreweryService breweryService;
        private readonly ICountryService countryService;

        public BreweriesController(IBreweryService breweryService, ICountryService countryService)
        {
            this.breweryService = breweryService ?? throw new ArgumentNullException(nameof(breweryService));
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            var breweries = await this.breweryService.GetAllBreweriesAsync();
            var result = breweries.GetViewModels();
            return View(result);
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var brewery = await this.breweryService.GetBreweryAsync(id);
            var result = brewery.GetViewModel();
            if (brewery == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Breweries/Create
        public async Task<IActionResult> Create()
        {
            var brewery = new BreweryViewModel();
            var countries = await this.countryService.GetAllCountriesAsync();

            brewery.Countries = countries.Select(b => new SelectListItem(b.Name, b.Name)).ToList();

            return View(brewery);
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Breweries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreweryViewModel brewery)
        {
            if (ModelState.IsValid)
            {
                var dto = brewery.GetDTO();
                await this.breweryService.CreateBreweryAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(brewery);
        }
        [Authorize(Roles = "Admin")]
        // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var brewery = await this.breweryService.GetBreweryAsync(id);
            var result = brewery.GetViewModel();
            if (brewery == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Breweries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BreweryViewModel brewery)
        {
            if (id != brewery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.breweryService.UpdateBreweryAsync(id, brewery.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(brewery);
        }
        [Authorize(Roles = "Admin")]
        // GET: Breweries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brewery = await this.breweryService.GetBreweryAsync(id);
            var result = brewery.GetViewModel();

            if (brewery == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.breweryService.DeleteBreweryAsync(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}
