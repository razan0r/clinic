using System.ComponentModel.DataAnnotations;

namespace WebClinicaMVC.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Specialty must contain between 3 and 50 characters.")]
        public string Name { get; set; }

        public List<Profissional> Profissionais { get; set; }
        public List<ProfissionalSpecialty> ProfissionalSpecialtys { get; set; }
    }
}
