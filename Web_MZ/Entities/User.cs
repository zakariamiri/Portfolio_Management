using System.ComponentModel.DataAnnotations;

namespace Web_MZ.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;

        // "Creator" ou "Recruiter"
        public string Role { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Creator? CreatorProfile { get; set; }
        public Recruiter? RecruiterProfile { get; set; }
    }
}
