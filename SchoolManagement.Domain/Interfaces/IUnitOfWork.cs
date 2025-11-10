using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>Unit of Work interface</summary>
public interface IUnitOfWork : IDisposable
{
    IStudentRepository Students { get; }
    ITeacherRepository Teachers { get; }
    ICourseRepository Courses { get; }
    IEnrollmentRepository Enrollments { get; }
    Task<int> SaveChangesAsync();
}
