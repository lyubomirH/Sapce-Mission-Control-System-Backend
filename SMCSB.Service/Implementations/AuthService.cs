using Microsoft.EntityFrameworkCore;
using SMCSB.Data.Configurations;
using SMCSB.Service.Contracts;
using SMCSB.Service.DTOs;

namespace SMCSB.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                // Find user by email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                // Check if user exists
                if (user == null)
                {
                    return new LoginResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid email or password"
                    };
                }

                // Verify password (plain text comparison - NOT RECOMMENDED FOR PRODUCTION)
                if (user.Password != loginDto.Password)
                {
                    return new LoginResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid email or password"
                    };
                }

                // Login successful
                return new LoginResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Role,
                    Message = "Login successful",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}