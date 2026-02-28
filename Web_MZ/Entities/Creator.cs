using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Creator
    {
        [Key]
        public int Id { get; set; }   // PK

        [ForeignKey("User")]
        public int UserId { get; set; } // FK vers User

        [StringLength(500)]
        public string Bio { get; set; }  // Présentation du créateur

        [StringLength(200)]
        public string ProfileImage { get; set; } // URL ou nom du fichier de l'image

        [StringLength(200)]
        public string LinkedIn { get; set; } // URL du profil LinkedIn

        [StringLength(200)]
        public string GitHub { get; set; } // URL du profil GitHub

        // Relation avec User
        public User User { get; set; }

        // Relations 1-N
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
        public ICollection<Competence> Competences { get; set; } = new List<Competence>();
        public ICollection<Langue> Langues { get; set; } = new List<Langue>();

    }
}
