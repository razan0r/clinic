using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace WebClinicaMVC.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Display(Name = "Profissional")]
        public int ProfissionalId { get; set; }
        public Profissional? Profissional { get; set; }

        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        [Display(Name = "Data e Time")]
        public DateTime DataTimeAppointment { get; set; }
    }
}
