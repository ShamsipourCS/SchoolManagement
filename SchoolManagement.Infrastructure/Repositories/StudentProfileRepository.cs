using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class StudentProfileRepository : GenericRepository<StudentProfile>, IStudentProfileRepository
{
    public StudentProfileRepository(SchoolDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves the enrollment with full details
    /// </summary>
    public async Task<StudentProfile?> GetWithEnrollmentsAsync(int id)
    {
        return await _dbSet
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .ThenInclude(c => c.TeacherProfile)
            .ThenInclude(tp => tp.User.Username)
            .FirstOrDefaultAsync(s => s.Id == id);
    }


    /// <summary>
    /// Retrieves all active student profiles (based on associated User.IsActive)
    /// </summary>
    public async Task<IEnumerable<StudentProfile>> GetActiveStudentProfilesAsync()
    {
        return await _dbSet
            .Where(s => s.User.IsActive) // Only need IsActive from User
            .OrderBy(s => s.FullName)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a student profile by the associated UserId
    /// </summary>
    public async Task<StudentProfile?> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }
}