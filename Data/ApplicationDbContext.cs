using clinic.Areas.Identity.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using WebClinicaMVC.Models;

namespace WebClinicaMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<clinicUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<ProfissionalSpecialty> ProfissionalSpecialtys { get; set; }
        public DbSet<Specialty> Specialtys { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Profissional>()
                .HasMany(e => e.Specialtys)
                .WithMany(p => p.Profissionais)
                .UsingEntity<ProfissionalSpecialty>(
                l => l.HasOne<Specialty>(e => e.Specialty).WithMany(e => e.ProfissionalSpecialtys).HasForeignKey(e => e.IdSpecialty),
                r => r.HasOne<Profissional>(e => e.Profissional).WithMany(e => e.ProfissionalSpecialtys).HasForeignKey(e => e.IdProfissional));

            base.OnModelCreating(builder);
        }
    }
}