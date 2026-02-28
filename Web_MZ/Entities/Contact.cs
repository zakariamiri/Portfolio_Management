using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Contact
    {
        [Key] // Clé primaire
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }  // Contenu du message envoyé

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateSent { get; set; } = DateTime.Now;  // Date et heure d’envoi

        // Relation avec Recruiter (expéditeur)
        [Required]
        public int RecruiterId { get; set; }

        [ForeignKey("RecruiterId")]
        public Recruiter Recruiter { get; set; }  // Navigation property vers le recruteur

        // Relation avec Creator (destinataire)
        [Required]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }  // Navigation property vers le créateur
    }
}
