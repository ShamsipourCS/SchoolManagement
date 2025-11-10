using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
}
