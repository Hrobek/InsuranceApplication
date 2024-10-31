using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsureApp.Data;
using InsureApp.Models;
using X.PagedList.Extensions;

namespace InsureApp.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public async Task<IActionResult> Index(int? page)
        {
            // Získání seznamu pojištěnců jako IQueryable
            var insurancesList = await _context.Insurances.ToListAsync();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Vytvoření paginovaného seznamu
            var pagedList = insurancesList.ToPagedList(pageNumber, pageSize);

            return View(pagedList); // Vraťte paginovaný seznam
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurances = await _context.Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurances == null)
            {
                return NotFound();
            }

            return View(insurances);
        }

        // GET: Insurances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Insurances insurances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insurances);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurances = await _context.Insurances.FindAsync(id);
            if (insurances == null)
            {
                return NotFound();
            }
            return View(insurances);
        }

        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Insurances insurances)
        {
            if (id != insurances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsurancesExists(insurances.Id))
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
            return View(insurances);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurances = await _context.Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurances == null)
            {
                return NotFound();
            }

            return View(insurances);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurances = await _context.Insurances.FindAsync(id);
            if (insurances != null)
            {
                _context.Insurances.Remove(insurances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsurancesExists(int id)
        {
            return _context.Insurances.Any(e => e.Id == id);
        }
    }
}
