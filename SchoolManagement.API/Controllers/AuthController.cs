using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// API controller for authentication and user registration
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the AuthController
    /// </summary>
    /// <param name="authService">Authentication service for user operations</param>
    /// <param name="logger">Logger instance</param>
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <param name="registerDto">User registration data</param>
    /// <returns>Authentication response with JWT token</returns>
    /// <response code="201">Returns the authentication response with token</response>
    /// <response code="400">If the registration data is invalid or user already exists</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for user registration");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Attempting to register user: {Username}", registerDto.Username);

        // Check if username already exists
        if (await _authService.UsernameExistsAsync(registerDto.Username))
        {
            _logger.LogWarning("Registration failed: Username {Username} already exists", registerDto.Username);
            return BadRequest(new { message = "Username already exists" });
        }

        // Check if email already exists
        if (await _authService.EmailExistsAsync(registerDto.Email))
        {
            _logger.LogWarning("Registration failed: Email {Email} already exists", registerDto.Email);
            return BadRequest(new { message = "Email already exists" });
        }

        try
        {
            var response = await _authService.RegisterAsync(registerDto);
            _logger.LogInformation("User {Username} registered successfully", registerDto.Username);

            return CreatedAtAction(
                nameof(Register),
                new { username = response.Username },
                response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during user registration for {Username}", registerDto.Username);
            return BadRequest(new { message = "Registration failed. Please try again." });
        }
    }

    /// <summary>
    /// Authenticate user and generate JWT token
    /// </summary>
    /// <param name="loginDto">User login credentials</param>
    /// <returns>Authentication response with JWT token</returns>
    /// <response code="200">Returns the authentication response with token</response>
    /// <response code="400">If the login data is invalid</response>
    /// <response code="401">If the credentials are incorrect</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for user login");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Login attempt for user: {Username}", loginDto.Username);

        try
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response == null)
            {
                _logger.LogWarning("Login failed for user: {Username} - Invalid credentials", loginDto.Username);
                return Unauthorized(new { message = "Invalid username or password" });
            }

            _logger.LogInformation("User {Username} logged in successfully", loginDto.Username);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login for user: {Username}", loginDto.Username);
            return BadRequest(new { message = "Login failed. Please try again." });
        }
    }
}
