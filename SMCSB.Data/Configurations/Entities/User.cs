using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SMCSB.Data.Configurations.Entities
{
        public class User
        {
            [Key]
            public int Id { get; set; }

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
            [MaxLength(20)]
            public string Role { get; set; } = "User";

            [Range(1, 120)]
            public int Age { get; set; }
        }
    }
