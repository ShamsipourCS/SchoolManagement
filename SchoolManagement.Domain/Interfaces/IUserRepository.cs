using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Get user by username
    /// </summary>
    /// <param name="username">Username to search for</param>
    /// <returns>User entity if found, null otherwise</returns>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Get user by email address
    /// </summary>
    /// <param name="email">Email address to search for</param>
    /// <returns>User entity if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if username already exists
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <param name="excludeUserId">Optional user ID to exclude from check (for updates)</param>
    /// <returns>True if username exists, false otherwise</returns>
    Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null);

    /// <summary>
    /// Check if email already exists
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <param name="excludeUserId">Optional user ID to exclude from check (for updates)</param>
    /// <returns>True if email exists, false otherwise</returns>
    Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);
}
