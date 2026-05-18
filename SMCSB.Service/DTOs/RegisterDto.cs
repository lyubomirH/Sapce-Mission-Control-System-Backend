using System.ComponentModel.DataAnnotations;

namespace SMCSB.Service.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;

        [Range(1, 120)]
        public int Age { get; set; }

        [MaxLength(20)]
        public string Role { get; set; } = "User";
    }
}