using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinicaMVC.Data;
using WebClinicaMVC.Models;

namespace WebClinicaMVC.Controllers
{
    [Authorize(Roles = "Admin")] // Protect the entire controller
    public class ProfissionaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfissionaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profissionals
        public async Task<IActionResult> Index()
        {
            return _context.Profissionais != null ?
                       View(await _context.Profissionais.ToListAsync()) :
                       Problem("Entity set 'ApplicationDbContext.Profissionais'  is null.");
        }

        // GET: Profissionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // GET: Profissionals/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profissional);
        }

        // GET: Profissionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        // POST: Profissionals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Profissional profissional)
        {
            if (id != profissional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalExists(profissional.Id))
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
            return View(profissional);
        }

        // GET: Profissionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // POST: Profissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profissionais == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Profissionais'  is null.");
            }
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional != null)
            {
                _context.Profissionais.Remove(profissional);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalExists(int id)
        {
            return (_context.Profissionais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
