using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _context;
    private IStudentProfileRepository? _studentProfiles;
    private ITeacherProfileRepository? _teacherProfiles;
    private ICourseRepository? _courses;
    private IEnrollmentRepository? _enrollments;
    private IUserRepository? _users;

    public UnitOfWork(SchoolDbContext context) => _context = context;

    public IStudentProfileRepository StudentProfiles => _studentProfiles ??= new StudentProfileRepository(_context);
    public ITeacherProfileRepository TeacherProfiles => _teacherProfiles ??= new TeacherProfileRepository(_context);

    public ICourseRepository Courses => _courses ??= new CourseRepository(_context);
    public IEnrollmentRepository Enrollments => _enrollments ??= new EnrollmentRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}