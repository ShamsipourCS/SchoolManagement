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
            var t1 = Teacher.Create("Teacher One", "t1@example.com", DateTime.UtcNow.AddYears(-5));
            var t2 = Teacher.Create("Teacher Two", "t2@example.com", DateTime.UtcNow.AddYears(-3));
            context.Teachers.AddRange(t1, t2);
            context.SaveChanges();

            var s1 = Student.Create("Student One", DateTime.UtcNow.AddYears(-20));
            var s2 = Student.Create("Student Two", DateTime.UtcNow.AddYears(-19));
            var s3 = Student.Create("Student Three", DateTime.UtcNow.AddYears(-21));
            context.Students.AddRange(s1, s2, s3);
            context.SaveChanges();

            var c1 = Course.Create("Math 101", t1.Id, DateTime.UtcNow.AddDays(-10));
            var c2 = Course.Create("Physics 101", t2.Id, DateTime.UtcNow.AddDays(-5));
            var c3 = Course.Create("Chemistry 101", t1.Id, DateTime.UtcNow.AddDays(1));
            context.Courses.AddRange(c1, c2, c3);
            context.SaveChanges();
        }
    }
}
