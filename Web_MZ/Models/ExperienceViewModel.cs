using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_MZ.Entities;

namespace Web_MZ.Models
{
    public class ExperienceViewModel
    {
    
        public int Id { get; set; }
        public string Title { get; set; }       // Titre du poste, ex: Développeur FullStack
        public string Company { get; set; }     // Nom de l’entreprise
        public string Location { get; set; }    // Localisation, ex: Paris, Maroc
        public DateTime StartDate { get; set; } // Date de début
        public DateTime? EndDate { get; set; }  // Date de fin, null = actuel
        public string Description { get; set; } // Description du poste / missions
        public int CreatorId { get; set; }
    }
}
