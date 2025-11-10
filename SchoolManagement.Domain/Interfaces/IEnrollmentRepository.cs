using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);
    Task<bool> IsEnrolledAsync(int studentId, int courseId);
    Task<Enrollment?> GetWithDetailsAsync(int id);
}
