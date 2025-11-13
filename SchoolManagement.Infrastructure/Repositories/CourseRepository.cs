using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(SchoolDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a course with its related teacher profile and enrolled students
    /// </summary>
    public async Task<Course?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.TeacherProfile)
            .ThenInclude(tp => tp.User.Username)
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.StudentProfile)
            .ThenInclude(sp => sp.User.Username)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Retrieves courses taught by a specific teacher (by teacher profile id)
    /// </summary>
    public async Task<IEnumerable<Course>> GetByTeacherProfileIdAsync(int teacherProfileId)
    {
        return await _dbSet
            .Where(c => c.TeacherProfileId == teacherProfileId)
            .OrderBy(c => c.Title)
            .ToListAsync();
    }
}