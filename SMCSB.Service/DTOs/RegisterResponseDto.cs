namespace SMCSB.Service.DTOs
{
    public class RegisterResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}