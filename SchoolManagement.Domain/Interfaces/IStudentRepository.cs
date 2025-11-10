using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Student-specific repository interface
/// </summary>
public interface IStudentRepository : IGenericRepository<Student>
{
    /// <summary>
    /// Get student with all enrollments and course details
    /// </summary>
    Task<Student?> GetWithEnrollmentsAsync(int id);

    /// <summary>
    /// Get all active students
    /// </summary>
    Task<IEnumerable<Student>> GetActiveStudentsAsync();
}