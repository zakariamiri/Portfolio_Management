using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Recruiter
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // FK vers User

        [StringLength(500)]
        public string Bio { get; set; }          // Description ou présentation du recruteur

        [StringLength(200)]
        public string CompanyName { get; set; }  // Nom de l'entreprise

        [StringLength(100)]
        public string Position { get; set; }     // Poste du recruteur

        [StringLength(200)]
        public string ProfileImage { get; set; } // URL ou nom du fichier de l'image

        // Relation avec User
        public User User { get; set; }           // Navigation property vers User
    }
}
