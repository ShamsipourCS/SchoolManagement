using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Enrollment-specific repository interface
/// </summary>
public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    /// <summary>
    /// Get all enrollments for a specific student
    /// </summary>
    Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);

    /// <summary>
    /// Get all enrollments for a specific course
    /// </summary>
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);

    /// <summary>
    /// Check if student is already enrolled in a course
    /// </summary>
    Task<bool> IsEnrolledAsync(int studentId, int courseId);

    /// <summary>
    /// Get enrollment with student and course details
    /// </summary>
    Task<Enrollment?> GetWithDetailsAsync(int id);
}