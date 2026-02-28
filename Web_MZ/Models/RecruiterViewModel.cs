using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_MZ.Entities;

namespace Web_MZ.Models
{
    public class RecruiterViewModel
    {
        public string Bio { get; set; }          // Description ou présentation du recruteur
        public string CompanyName { get; set; }  // Nom de l'entreprise
        public string Position { get; set; }     // Poste du recruteur
        public IFormFile ProfileImage { get; set; } // URL ou nom du fichier de l'image

    }
}
