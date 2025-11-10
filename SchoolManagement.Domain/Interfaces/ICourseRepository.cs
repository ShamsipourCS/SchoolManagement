using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Course-specific repository interface
/// </summary>
public interface ICourseRepository : IGenericRepository<Course>
{
    /// <summary>
    /// Get course with teacher and enrollment details
    /// </summary>
    Task<Course?> GetWithDetailsAsync(int id);

    /// <summary>
    /// Get all courses taught by a specific teacher
    /// </summary>
    Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
}