using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Utilities;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Application.Services;

/// <summary>
/// Service for user authentication and registration
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>
    /// Registers a new user with the specified credentials
    /// </summary>
    /// <param name="registerDto">Registration information</param>
    /// <returns>Authentication response with JWT token</returns>
    /// <exception cref="InvalidOperationException">Thrown when username or email already exists</exception>
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto)
    {
        // Check if username already exists
        if (await _unitOfWork.Users.UsernameExistsAsync(registerDto.Username))
        {
            throw new InvalidOperationException($"Username '{registerDto.Username}' is already taken");
        }

        // Check if email already exists
        if (await _unitOfWork.Users.EmailExistsAsync(registerDto.Email))
        {
            throw new InvalidOperationException($"Email '{registerDto.Email}' is already registered");
        }

        // Hash the password
        var passwordHash = PasswordHasher.HashPassword(registerDto.Password);

        // Create new user entity
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            Role = string.IsNullOrWhiteSpace(registerDto.Role) ? "User" : registerDto.Role,
            CreatedAt = DateTime.UtcNow
        };

        // Add user to repository
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user);

        // Return authentication response
        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    /// <summary>
    /// Authenticates a user and generates a JWT token
    /// </summary>
    /// <param name="loginDto">Login credentials</param>
    /// <returns>Authentication response with JWT token, or null if authentication fails</returns>
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto)
    {
        // Find user by username
        var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);

        // Return null if user not found
        if (user == null)
        {
            return null;
        }

        // Verify password
        if (!PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user);

        // Return authentication response
        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    /// <summary>
    /// Checks if a username already exists
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if username exists, false otherwise</returns>
    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _unitOfWork.Users.UsernameExistsAsync(username);
    }

    /// <summary>
    /// Checks if an email already exists
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>True if email exists, false otherwise</returns>
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _unitOfWork.Users.EmailExistsAsync(email);
    }
}
