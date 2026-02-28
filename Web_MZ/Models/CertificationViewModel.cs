using System.ComponentModel.DataAnnotations;

namespace Web_MZ.Models
{
    public class CertificationViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Issuer { get; set; }

        public string CredentialUrl { get; set; }

        public string CredentialId { get; set; }

        public DateTime DateIssued { get; set; }

        public int CreatorId { get; set; }
    }
}
