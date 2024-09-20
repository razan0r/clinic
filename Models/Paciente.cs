using System.ComponentModel.DataAnnotations;

namespace WebClinicaMVC.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
