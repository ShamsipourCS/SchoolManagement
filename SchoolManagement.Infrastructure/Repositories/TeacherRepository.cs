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

public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(SchoolDbContext context) : base(context) { }

    public async Task<Teacher?> GetWithCoursesAsync(int id)
    {
        return await _dbSet
            .Include(t => t.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Teacher?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null)
    {
        return await _dbSet.AnyAsync(t => t.Email == email && (!excludeTeacherId.HasValue || t.Id != excludeTeacherId.Value));
    }
}
