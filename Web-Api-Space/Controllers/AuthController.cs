using Microsoft.AspNetCore.Mvc;
using SMCSB.Service.DTOs;
using SMCSB.Service.Services;

namespace Web_Api_Space.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user and returns user information
        /// </summary>
        /// <param name="loginDto">User login credentials</param>
        /// <returns>User information if successful</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Invalid request data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Attempt login
            var result = await _authService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(new
                {
                    result.IsSuccess,
                    result.Message
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerDto">User registration details</param>
        /// <returns>Registration result</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Invalid request data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Attempt registration
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
            {
                // Check if it's a conflict (email or username exists)
                if (result.Message.Contains("already registered") || result.Message.Contains("already taken"))
                {
                    return Conflict(new
                    {
                        result.IsSuccess,
                        result.Message
                    });
                }

                return BadRequest(new
                {
                    result.IsSuccess,
                    result.Message
                });
            }

            // Return 201 Created with location header
            return CreatedAtAction(nameof(Register), new { id = result.UserId }, result);
        }
    }
}