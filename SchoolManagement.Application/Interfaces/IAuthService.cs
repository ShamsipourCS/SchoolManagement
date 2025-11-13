using SchoolManagement.Application.DTOs.Auth;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service for user authentication and registration
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user with the specified credentials
    /// </summary>
    /// <param name="registerDto">Registration information</param>
    /// <returns>Authentication response with JWT token</returns>
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto);

    /// <summary>
    /// Authenticates a user and generates a JWT token
    /// </summary>
    /// <param name="loginDto">Login credentials</param>
    /// <returns>Authentication response with JWT token, or null if authentication fails</returns>
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto);

    /// <summary>
    /// Checks if a username already exists
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if username exists, false otherwise</returns>
    Task<bool> UsernameExistsAsync(string username);

    /// <summary>
    /// Checks if an email already exists
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>True if email exists, false otherwise</returns>
    Task<bool> EmailExistsAsync(string email);
}
