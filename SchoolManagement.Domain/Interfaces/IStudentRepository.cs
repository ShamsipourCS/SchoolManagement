using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<Student?> GetWithEnrollmentsAsync(int id);
    Task<IEnumerable<Student>> GetActiveStudentsAsync();
}
