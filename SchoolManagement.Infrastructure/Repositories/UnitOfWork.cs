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
    private IStudentRepository? _students;
    private ITeacherRepository? _teachers;
    private ICourseRepository? _courses;
    private IEnrollmentRepository? _enrollments;
    private IUserRepository? _users;

    public UnitOfWork(SchoolDbContext context) => _context = context;

    public IStudentRepository Students => _students ??= new StudentRepository(_context);
    public ITeacherRepository Teachers => _teachers ??= new TeacherRepository(_context);
    public IStudentProfileRepository StudentProfiles { get; }
    public ITeacherProfileRepository TeacherProfiles { get; }
    public ICourseRepository Courses => _courses ??= new CourseRepository(_context);
    public IEnrollmentRepository Enrollments => _enrollments ??= new EnrollmentRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}