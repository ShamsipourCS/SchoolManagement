using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for User entity
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SchoolDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get user by username
    /// </summary>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Get user by email address
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Check if username already exists
    /// </summary>
    public async Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null)
    {
        if (excludeUserId.HasValue)
        {
            return await _dbSet
                .AnyAsync(u => u.Username == username && u.Id != excludeUserId.Value);
        }

        return await _dbSet
            .AnyAsync(u => u.Username == username);
    }

    /// <summary>
    /// Check if email already exists
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
    {
        if (excludeUserId.HasValue)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email && u.Id != excludeUserId.Value);
        }

        return await _dbSet
            .AnyAsync(u => u.Email == email);
    }
}
