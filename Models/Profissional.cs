using System.ComponentModel.DataAnnotations;


namespace WebClinicaMVC.Models
{
    public class Profissional
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public List<Specialty> Specialtys { get; } = new();

        public List<ProfissionalSpecialty> ProfissionalSpecialtys { get; set; } = null!;
    }
}
