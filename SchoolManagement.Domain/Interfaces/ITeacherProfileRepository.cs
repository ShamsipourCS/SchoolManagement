using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Teacher profile-specific repository interface
/// </summary>
public interface ITeacherProfileRepository : IGenericRepository<TeacherProfile>
{
    /// <summary>
    /// Get teacher profile with all courses
    /// </summary>
    Task<TeacherProfile?> GetWithCoursesAsync(int id);

    /// <summary>
    /// Get teacher profile by user ID
    /// </summary>
    Task<TeacherProfile?> GetByUserIdAsync(int userId);

    /// <summary>
    /// Get all active teacher profiles (based on User.IsActive)
    /// </summary>
    Task<IEnumerable<TeacherProfile>> GetActiveTeacherProfilesAsync();


    /// <summary>
    /// Get teacher profile by email address
    /// </summary>
    Task<TeacherProfile?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if email is already in use
    /// </summary>
    Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null);
}