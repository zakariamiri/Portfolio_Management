using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Project
    {
        [Key] // Clé primaire
        public int Id { get; set; }

        [StringLength(150)]
        public string Title { get; set; }       // Nom du projet

        [StringLength(2000)]
        public string Description { get; set; } // Description du projet

        [StringLength(500)]
        public string Technologies { get; set; } // Technologies utilisées (ex: C#, React)

        [Url]
        [StringLength(200)]
        public string Link { get; set; }        // Lien vers le projet (optionnel)

        [StringLength(200)]

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; } // Date de début (optionnelle)

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }   // Date de fin (optionnelle)

        // Relation avec Creator (1 - N)
        [Required]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }     // Navigation property vers le Creator
    }
}
