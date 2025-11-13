using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Student profile-specific repository interface
/// </summary>
public interface IStudentProfileRepository : IGenericRepository<StudentProfile>
{
    /// <summary>
    /// Get student profile with all enrollments and course details
    /// </summary>
    Task<StudentProfile?> GetWithEnrollmentsAsync(int id);

    /// <summary>
    /// Get student profile by user ID
    /// </summary>
    Task<StudentProfile?> GetByUserIdAsync(int userId);

    /// <summary>
    /// Get all active student profiles (based on User.IsActive)
    /// </summary>
    Task<IEnumerable<StudentProfile>> GetActiveStudentProfilesAsync();
}