namespace Web_MZ.Models
{
    public class MessageVM
    {

        public string RecruiterName { get; set; } = string.Empty; // Nom du recruteur
        public string Email { get; set; } = string.Empty;          // Email du recruiter
        public string Phone { get; set; } = string.Empty;          // Téléphone du recruiter

        public string Position { get; set; } = string.Empty;          

        public string MessageContent { get; set; } = string.Empty; // Contenu
        public DateTime DateSent { get; set; } // Date d'envoi
    }
}
