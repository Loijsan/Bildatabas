using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bildatabas.Data;
using Bildatabas.Models;

namespace Bildatabas.Controllers
{
    public class CarDealersController : Controller
    {
        private readonly CarContext _context;

        public CarDealersController(CarContext context)
        {
            _context = context;
        }

        // GET: CarDealers
        public async Task<IActionResult> Index()
        {
            var carContext = _context.CarDealers.Include(c => c.City);
            return View(await carContext.ToListAsync());
        }

        // GET: CarDealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carDealer = await _context.CarDealers
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carDealer == null)
            {
                return NotFound();
            }

            return View(carDealer);
        }

        // GET: CarDealers/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            return View();
        }

        // POST: CarDealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarDealerName,CityId")] CarDealer carDealer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carDealer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", carDealer.CityId);
            return View(carDealer);
        }

        // GET: CarDealers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carDealer = await _context.CarDealers.FindAsync(id);
            if (carDealer == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", carDealer.CityId);
            return View(carDealer);
        }

        // POST: CarDealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarDealerName,CityId")] CarDealer carDealer)
        {
            if (id != carDealer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carDealer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarDealerExists(carDealer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", carDealer.CityId);
            return View(carDealer);
        }

        // GET: CarDealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carDealer = await _context.CarDealers
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carDealer == null)
            {
                return NotFound();
            }

            return View(carDealer);
        }

        // POST: CarDealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carDealer = await _context.CarDealers.FindAsync(id);
            _context.CarDealers.Remove(carDealer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarDealerExists(int id)
        {
            return _context.CarDealers.Any(e => e.Id == id);
        }
    }
}
