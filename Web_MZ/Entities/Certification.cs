using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_MZ.Entities
{
    public class Certification
    {
        [Key] // Clé primaire
        public int Id { get; set; }

        public string Title { get; set; }   // Nom du certificat

        public string Issuer { get; set; }  // Organisation qui délivre le certificat


        public string CredentialUrl { get; set; } // Lien vers le certificat en ligne (facultatif)

        public string CredentialId { get; set; }  // Identifiant du certificat (optionnel)

  
        public DateTime DateIssued { get; set; }  // Date d’obtention du certificat

        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }   // Navigation property
    }
}
