using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCSB.Service.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
    }
}
