using SMCSB.Service.DTOs;

namespace SMCSB.Service.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}