using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Competence
    {
        [Key] // Clé primaire
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Nom de la compétence, ex: C#, React

        [Range(1, 5)]
        public int Level { get; set; }    // Niveau de compétence de 1 à 5

        [StringLength(50)]
        public string Category { get; set; } // Catégorie, ex: Backend, Frontend, Soft Skills

        // Relation avec Creator (1 - N)
        [Required]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; } // Navigation property vers le Creator
    }
}
