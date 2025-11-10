using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Teacher-specific repository interface
/// </summary>
public interface ITeacherRepository : IGenericRepository<Teacher>
{
    /// <summary>
    /// Get teacher with all courses
    /// </summary>
    Task<Teacher?> GetWithCoursesAsync(int id);

    /// <summary>
    /// Get teacher by email address
    /// </summary>
    Task<Teacher?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if email is already in use
    /// </summary>
    Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null);
}