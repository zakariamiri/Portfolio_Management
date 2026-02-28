using Web_MZ.Entities;

namespace Web_MZ.Models
{
    public class ConsulterPortfolioVM
    {
        public Creator Creator { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<Project> Projects { get; set; }
        public List<Competence> Competences { get; set; }
        public List<Langue> Langues { get; set; }
        public List<Certification> Certifications { get; set; }
    }
}
