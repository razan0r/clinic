
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinicaMVC.Data;
using WebClinicaMVC.Models;

namespace WebClinicaMVC.Controllers
{
    public class ProfissionalSpecialtysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfissionalSpecialtysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfissionalSpecialtys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfissionalSpecialtys.Include(p => p.Specialty).Include(p => p.Profissional);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProfissionalSpecialtys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProfissionalSpecialtys == null)
            {
                return NotFound();
            }

            var profissionalSpecialty = await _context.ProfissionalSpecialtys
                .Include(p => p.Specialty)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.IdSpecialty == id);
            if (profissionalSpecialty == null)
            {
                return NotFound();
            }

            return View(profissionalSpecialty);
        }

        // GET: ProfissionalSpecialtys/Create
        public IActionResult Create()
        {
            ViewData["IdSpecialty"] = new SelectList(_context.Specialtys, "Id", "Name");
            ViewData["IdProfissional"] = new SelectList(_context.Profissionais, "Id", "Id");
            return View();
        }

        // POST: ProfissionalSpecialtys/Create
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfissional,IdSpecialty")] ProfissionalSpecialty profissionalSpecialty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profissionalSpecialty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSpecialty"] = new SelectList(_context.Specialtys, "Id", "Name", profissionalSpecialty.IdSpecialty);
            ViewData["IdProfissional"] = new SelectList(_context.Profissionais, "Id", "Id", profissionalSpecialty.IdProfissional);
            return View(profissionalSpecialty);
        }

        // GET: ProfissionalSpecialtys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProfissionalSpecialtys == null)
            {
                return NotFound();
            }

            var profissionalSpecialty = await _context.ProfissionalSpecialtys.FindAsync(id);
            if (profissionalSpecialty == null)
            {
                return NotFound();
            }
            ViewData["IdSpecialty"] = new SelectList(_context.Specialtys, "Id", "Name", profissionalSpecialty.IdSpecialty);
            ViewData["IdProfissional"] = new SelectList(_context.Profissionais, "Id", "Id", profissionalSpecialty.IdProfissional);
            return View(profissionalSpecialty);
        }

        // POST: ProfissionalSpecialtys/Edit/5
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProfissional,IdSpecialty")] ProfissionalSpecialty profissionalSpecialty)
        {
            if (id != profissionalSpecialty.IdSpecialty)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissionalSpecialty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalSpecialtyExists(profissionalSpecialty.IdSpecialty))
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
            ViewData["IdSpecialty"] = new SelectList(_context.Specialtys, "Id", "Name", profissionalSpecialty.IdSpecialty);
            ViewData["IdProfissional"] = new SelectList(_context.Profissionais, "Id", "Id", profissionalSpecialty.IdProfissional);
            return View(profissionalSpecialty);
        }

        // GET: ProfissionalSpecialtys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProfissionalSpecialtys == null)
            {
                return NotFound();
            }

            var profissionalSpecialty = await _context.ProfissionalSpecialtys
                .Include(p => p.Specialty)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.IdSpecialty == id);
            if (profissionalSpecialty == null)
            {
                return NotFound();
            }

            return View(profissionalSpecialty);
        }

        // POST: ProfissionalSpecialtys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProfissionalSpecialtys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProfissionalSpecialtys'  is null.");
            }
            var profissionalSpecialty = await _context.ProfissionalSpecialtys.FindAsync(id);
            if (profissionalSpecialty != null)
            {
                _context.ProfissionalSpecialtys.Remove(profissionalSpecialty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalSpecialtyExists(int id)
        {
          return (_context.ProfissionalSpecialtys?.Any(e => e.IdSpecialty == id)).GetValueOrDefault();
        }
    }
}
