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
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
              return _context.Appointments != null ? 
                          View(await _context.Appointments
                          .Include(x => x.Profissional)
                          .Include(x => x.Paciente)
                          .ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var Appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Appointment == null)
            {
                return NotFound();
            }

            return View(Appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
     

            ViewBag.Profissionais = new SelectList(_context.Profissionais.AsNoTracking().ToList(), "Id", "Name");
            ViewBag.Pacientes = new SelectList(_context.Pacientes.AsNoTracking().ToList(), "Id", "Name");

            return View();
        }

        // POST: Appointments/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfissionalId,PacienteId,DataTimeAppointment")] Appointment Appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Profissionais = new SelectList(_context.Profissionais.AsNoTracking().ToList(), "Id", "Name");
            ViewBag.Pacientes = new SelectList(_context.Pacientes.AsNoTracking().ToList(), "Id", "Name");
            return View(Appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var Appointment = await _context.Appointments.FindAsync(id);
            if (Appointment == null)
            {
                return NotFound();
            }
            return View(Appointment);
        }

        // POST: Appointments/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfissionalId,PacienteId,DataTimeAppointment")] Appointment Appointment)
        {
            if (id != Appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(Appointment.Id))
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
            return View(Appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var Appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Appointment == null)
            {
                return NotFound();
            }

            return View(Appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
            }
            var Appointment = await _context.Appointments.FindAsync(id);
            if (Appointment != null)
            {
                _context.Appointments.Remove(Appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
