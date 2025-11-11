using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistence.Seed;

public static class DevelopmentSeed
{
    public static void Seed(SchoolDbContext context)
    {
        if (!context.Teachers.Any())
        {
            var t1 = new Teacher { FullName = "Teacher One", Email = new Email("t1@example.com"), HireDate = DateTime.UtcNow.AddYears(-5) };
            var t2 = new Teacher { FullName = "Teacher Two", Email = new Email("t2@example.com"), HireDate = DateTime.UtcNow.AddYears(-3) };
            context.Teachers.AddRange(t1, t2);
            context.SaveChanges();

            var s1 = new Student { FullName = "Student One", BirthDate = DateTime.UtcNow.AddYears(-20) };
            var s2 = new Student { FullName = "Student Two", BirthDate = DateTime.UtcNow.AddYears(-19) };
            var s3 = new Student { FullName = "Student Three", BirthDate = DateTime.UtcNow.AddYears(-21) };
            context.Students.AddRange(s1, s2, s3);
            context.SaveChanges();

            var c1 = new Course { Title = "Math 101", StartDate = DateTime.UtcNow.AddDays(-10), TeacherId = t1.Id };
            var c2 = new Course { Title = "Physics 101", StartDate = DateTime.UtcNow.AddDays(-5), TeacherId = t2.Id };
            var c3 = new Course { Title = "Chemistry 101", StartDate = DateTime.UtcNow.AddDays(1), TeacherId = t1.Id };
            context.Courses.AddRange(c1, c2, c3);
            context.SaveChanges();
        }
    }
}
