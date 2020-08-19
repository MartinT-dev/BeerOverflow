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
using BeerOverflow.Models;
using BeerOverflow.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var countries = await this.countryService.GetAllCountriesAsync();
            var result = countries.GetViewModels();

            return View(result);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var country = await this.countryService.GetCountryAsync(id);
            var result =country.GetViewModel();
               
            if (country == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Countries/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var dto = country.GetDTO();
                await this.countryService.CreateCountryAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        [Authorize(Roles = "Admin")]
        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var country = await this.countryService.GetCountryAsync(id);
            var result = country.GetViewModel();
            if (country == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryViewModel country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 
                await this.countryService.UpdateCountryAsync(id, country.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        [Authorize(Roles = "Admin")]
        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var country = await this.countryService.GetCountryAsync(id);
            var result = country.GetViewModel();
            if (country == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.countryService.DeleteCountryAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
