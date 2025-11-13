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

    public async Task<Course?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Teacher)
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId)
    {
        return await _dbSet
            .Where(c => c.TeacherId == teacherId)
            .OrderBy(c => c.Title)
            .ToListAsync();
    }
}