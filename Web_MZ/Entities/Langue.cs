using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Langue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }  // Exemple : "Français", "Anglais"

        [StringLength(50)]
        public string Niveau { get; set; } // Exemple : "Débutant", "Intermédiaire", "Avancé", "Fluent"

        // FK vers Creator
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }
    }
}
