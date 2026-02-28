using System.ComponentModel.DataAnnotations;

namespace Web_MZ.Models
{
    public class CompetenceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }  // Nom de la compétence, ex: C#, React

        public int Level { get; set; }    // Niveau de compétence de 1 à 5

        public string Category { get; set; } // Catégorie, ex: Backend, Frontend, Soft Skills
        public int CreatorId { get; set; }
    }
}
