namespace WebClinicaMVC.Models
{
    public class ProfissionalSpecialty

    {
        public int IdProfissional { get; set; }
        public int IdSpecialty { get; set; }
        public Profissional Profissional { get; set; }
        public Specialty Specialty { get; set; }
    }
}
