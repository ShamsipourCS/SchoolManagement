using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(SchoolDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Enrollment>> GetByStudentProfileIdAsync(int studentProfileId)
    {
        return await _dbSet
            .Include(e => e.Course)
            .ThenInclude(c => c.TeacherProfile)
            .ThenInclude(tp => tp.User.Username)
            .Where(e => e.StudentProfileId == studentProfileId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId)
    {
        return await _dbSet
            .Include(e => e.StudentProfile)
            .ThenInclude(sp => sp.User.Username)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task<bool> IsEnrolledAsync(int studentProfileId, int courseId)
    {
        return await _dbSet.AnyAsync(e => e.StudentProfileId == studentProfileId && e.CourseId == courseId);
    }

    public async Task<Enrollment?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(e => e.StudentProfile)
            .ThenInclude(sp => sp.User.Username)
            .Include(e => e.Course)
            .ThenInclude(c => c.TeacherProfile)
            .ThenInclude(tp => tp.User.Username)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}