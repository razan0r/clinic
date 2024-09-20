using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinicaMVC.Data;
using WebClinicaMVC.Models;

namespace WebClinicaMVC.Controllers
{
    public class SpecialtysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialtysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialtys
        public async Task<IActionResult> Index()
        {
              return _context.Specialtys != null ? 
                          View(await _context.Specialtys.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Specialtys'  is null.");
        }

        // GET: Specialtys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialtys == null)
            {
                return NotFound();
            }

            var Specialty = await _context.Specialtys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Specialty == null)
            {
                return NotFound();
            }

            return View(Specialty);
        }

        // GET: Specialtys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialtys/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Specialty Specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Specialty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Specialty);
        }

        // GET: Specialtys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialtys == null)
            {
                return NotFound();
            }

            var Specialty = await _context.Specialtys.FindAsync(id);
            if (Specialty == null)
            {
                return NotFound();
            }
            return View(Specialty);
        }

        // POST: Specialtys/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Specialty Specialty)
        {
            if (id != Specialty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Specialty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialtyExists(Specialty.Id))
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
            return View(Specialty);
        }

        // GET: Specialtys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialtys == null)
            {
                return NotFound();
            }

            var Specialty = await _context.Specialtys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Specialty == null)
            {
                return NotFound();
            }

            return View(Specialty);
        }

        // POST: Specialtys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialtys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Specialtys'  is null.");
            }
            var Specialty = await _context.Specialtys.FindAsync(id);
            if (Specialty != null)
            {
                _context.Specialtys.Remove(Specialty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialtyExists(int id)
        {
          return (_context.Specialtys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
