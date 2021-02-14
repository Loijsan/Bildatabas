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
    public class CarsController : Controller
    {
        private readonly CarContext _context;

        public CarsController(CarContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var carContext = _context.Cars.Include(c => c.CarDealer).Include(c => c.Engine).Include(c => c.Manufacturer);
            return View(await carContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarDealer)
                .Include(c => c.Engine)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CardealerId"] = new SelectList(_context.CarDealers, "Id", "CarDealerName");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "EngineType");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "ManufacturerName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ManufacturerId,CarModel,EngineId,ProductionYear,CarPrice,CardealerId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardealerId"] = new SelectList(_context.CarDealers, "Id", "CarDealerName", car.CardealerId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "EngineType", car.EngineId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "ManufacturerName", car.ManufacturerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CardealerId"] = new SelectList(_context.CarDealers, "Id", "CarDealerName", car.CardealerId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "EngineType", car.EngineId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "ManufacturerName", car.ManufacturerId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ManufacturerId,CarModel,EngineId,ProductionYear,CarPrice,CardealerId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CardealerId"] = new SelectList(_context.CarDealers, "Id", "CarDealerName", car.CardealerId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "EngineType", car.EngineId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "ManufacturerName", car.ManufacturerId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarDealer)
                .Include(c => c.Engine)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
