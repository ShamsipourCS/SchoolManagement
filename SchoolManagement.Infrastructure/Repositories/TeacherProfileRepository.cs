using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class TeacherProfileRepository : GenericRepository<TeacherProfile>, ITeacherProfileRepository
{
    public TeacherProfileRepository(SchoolDbContext context) : base(context)
    {
    }

    public async Task<TeacherProfile?> GetWithCoursesAsync(int id)
    {
        return await _dbSet
            .Include(t => t.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Retrieves a teacher profile by the associated UserId
    /// </summary>
    public async Task<TeacherProfile?> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }

    /// <summary>
    /// Retrieves all active teacher profiles (based on associated User.IsActive)
    /// </summary>
    public async Task<IEnumerable<TeacherProfile>> GetActiveTeacherProfilesAsync()
    {
        return await _dbSet
            .Where(t => t.User.IsActive)
            .OrderBy(t => t.FullName)
            .ToListAsync();
    }

    // <summary>
    /// Retrieves a teacher profile by the associated User email
    /// </summary>
    public async Task<TeacherProfile?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        email = email.Trim().ToLowerInvariant();

        return await _dbSet
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.User.Email.ToLower() == email);
    }

    /// <summary>
    /// Checks if an email exists, optionally excluding a specific teacher profile
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email, int? excludeTeacherProfileId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        email = email.Trim().ToLowerInvariant();

        return await _dbSet
            .Include(t => t.User)
            .AnyAsync(t => t.User.Email.ToLower() == email &&
                           (!excludeTeacherProfileId.HasValue || t.Id != excludeTeacherProfileId.Value));
    }
}