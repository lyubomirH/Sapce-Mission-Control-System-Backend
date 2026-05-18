using Microsoft.EntityFrameworkCore;
using SMCSB.Data.Configurations;
using SMCSB.Data.Configurations.Entities;
using SMCSB.Data ;
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

        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                // Check if email already exists
                var existingEmail = await _context.Users
                    .AnyAsync(u => u.Email == registerDto.Email);

                if (existingEmail)
                {
                    return new RegisterResponseDto
                    {
                        IsSuccess = false,
                        Message = "Email is already registered"
                    };
                }

                // Check if username already exists
                var existingUsername = await _context.Users
                    .AnyAsync(u => u.Username == registerDto.Username);

                if (existingUsername)
                {
                    return new RegisterResponseDto
                    {
                        IsSuccess = false,
                        Message = "Username is already taken"
                    };
                }

                // Create new user
                var user = new User
                {
                    Email = registerDto.Email,
                    Username = registerDto.Username,
                    Password = registerDto.Password, // In production, hash this password!
                    Role = string.IsNullOrEmpty(registerDto.Role) ? "User" : registerDto.Role,
                    Age = registerDto.Age
                };

                // Add user to database
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new RegisterResponseDto
                {
                    IsSuccess = true,
                    Message = "User registered successfully",
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.Username
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred during registration: {ex.Message}"
                };
            }
        }
    }
}