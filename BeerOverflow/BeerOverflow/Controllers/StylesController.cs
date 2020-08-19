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
    public class StylesController : Controller
    {
        private readonly IStyleService styleService;

        public StylesController(IStyleService styleService)
        {
            this.styleService = styleService ?? throw new ArgumentNullException(nameof(styleService));
        }

        // GET: Styles
        public async Task<IActionResult> Index()
        {
            var styles = await this.styleService.GetAllStylesAsync();
            var result = styles.GetViewModels();
            return View(result);
        }

        // GET: Styles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var style = await this.styleService.GetStyleAsync(id);
            var result = style.GetViewModel();
            if (style == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Styles/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        // POST: Styles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StyleViewModel style)
        {
            if (ModelState.IsValid)
            {
                var dto = style.GetDTO();
                await this.styleService.CreateStyleAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(style);
        }
        [Authorize(Roles = "Admin")]
        // GET: Styles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var style = await this.styleService.GetStyleAsync(id);
            var result = style.GetViewModel();
            if (style == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Styles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StyleViewModel style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.styleService.UpdateStyleAsync(id, style.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(style);
        }
        [Authorize(Roles = "Admin")]
        // GET: Styles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var style = await this.styleService.GetStyleAsync(id);
            var result = style.GetViewModel();
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.styleService.DeleteStyleAsync(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}
