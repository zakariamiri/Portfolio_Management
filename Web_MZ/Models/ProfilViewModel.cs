using Web_MZ.Entities;

namespace Web_MZ.Models
{
    public class ProfilViewModel
    {
        public Creator Creator { get; set; }
        public List<Experience> Experiences { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Competence> Competences { get; set; } = new();
        public List<Certification> Certifications { get; set; } = new();
        public List<Langue> Langues { get; set; } = new();
    }
}
