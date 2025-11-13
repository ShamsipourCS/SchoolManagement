using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Unit of Work interface for managing transactions across multiple repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Student profile repository
    /// </summary>
    IStudentProfileRepository StudentProfiles { get; }

    /// <summary>
    /// Teacher profile repository
    /// </summary>
    ITeacherProfileRepository TeacherProfiles { get; }

    /// <summary>
    /// Course repository
    /// </summary>
    ICourseRepository Courses { get; }

    /// <summary>
    /// Enrollment repository
    /// </summary>
    IEnrollmentRepository Enrollments { get; }

    /// <summary>
    /// User repository
    /// </summary>
    IUserRepository Users { get; }

    /// <summary>
    /// Save all changes to the database asynchronously
    /// </summary>
    Task<int> SaveChangesAsync();
}