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
    public EnrollmentRepository(SchoolDbContext context) : base(context) { }

    public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
    {
        return await _dbSet
            .Include(e => e.Course)
            .ThenInclude(c => c.Teacher)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId)
    {
        return await _dbSet
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task<bool> IsEnrolledAsync(int studentId, int courseId)
    {
        return await _dbSet.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
    }

    public async Task<Enrollment?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(e => e.Student)
            .Include(e => e.Course)
                .ThenInclude(c => c.Teacher)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
