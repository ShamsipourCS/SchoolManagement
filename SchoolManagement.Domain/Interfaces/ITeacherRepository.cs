using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

public interface ITeacherRepository : IGenericRepository<Teacher>
{
    Task<Teacher?> GetWithCoursesAsync(int id);
    Task<Teacher?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null);
}
