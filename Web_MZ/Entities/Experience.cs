using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Experience
    {
        [Key] // Clé primaire
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }       // Titre du poste, ex: Développeur FullStack

        [Required]
        [StringLength(100)]
        public string Company { get; set; }     // Nom de l’entreprise

        [StringLength(100)]
        public string Location { get; set; }    // Localisation, ex: Paris, Maroc

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } // Date de début

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }  // Date de fin, null = actuel

        [StringLength(1000)]
        public string Description { get; set; } // Description du poste / missions

        // Relation avec Creator (1 - N)
        [Required]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }    // Navigation property vers le Creator
    }
}
