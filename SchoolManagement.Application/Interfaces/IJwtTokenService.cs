using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service for generating and managing JWT tokens
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user
    /// </summary>
    /// <param name="user">User entity containing authentication information</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(User user);
}
