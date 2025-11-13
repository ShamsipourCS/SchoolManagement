using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistence.Seed;

public static class DevelopmentSeed
{
    public static void Seed(SchoolDbContext context)
    {
        if (!context.Users.Any())
        {
            var teacherUser1 = User.Create("teacher1", "t1@example.com", "hashedpassword", UserRole.Teacher);
            var teacherUser2 = User.Create("teacher2", "t2@example.com", "hashedpassword", UserRole.Teacher);
            context.Users.AddRange(teacherUser1, teacherUser2);
            context.SaveChanges();

            var teacherProfile1 = TeacherProfile.Create(teacherUser1.Id, "Teacher One", DateTime.UtcNow.AddYears(-5));
            var teacherProfile2 = TeacherProfile.Create(teacherUser2.Id, "Teacher Two", DateTime.UtcNow.AddYears(-3));
            context.TeacherProfiles.AddRange(teacherProfile1, teacherProfile2);
            context.SaveChanges();

            var studentUser1 = User.Create("student1", "s1@example.com", "hashedpassword", UserRole.Student);
            var studentUser2 = User.Create("student2", "s2@example.com", "hashedpassword", UserRole.Student);
            var studentUser3 = User.Create("student3", "s3@example.com", "hashedpassword", UserRole.Student);
            context.Users.AddRange(studentUser1, studentUser2, studentUser3);
            context.SaveChanges();

            var studentProfile1 = StudentProfile.Create(studentUser1.Id, "Student One", DateTime.UtcNow.AddYears(-20));
            var studentProfile2 = StudentProfile.Create(studentUser2.Id, "Student Two", DateTime.UtcNow.AddYears(-19));
            var studentProfile3 =
                StudentProfile.Create(studentUser3.Id, "Student Three", DateTime.UtcNow.AddYears(-21));
            context.StudentProfiles.AddRange(studentProfile1, studentProfile2, studentProfile3);
            context.SaveChanges();

            var c1 = Course.Create("Math 101", teacherProfile1.Id, DateTime.UtcNow.AddDays(-10));
            var c2 = Course.Create("Physics 101", teacherProfile2.Id, DateTime.UtcNow.AddDays(-5));
            var c3 = Course.Create("Chemistry 101", teacherProfile1.Id, DateTime.UtcNow.AddDays(1));
            context.Courses.AddRange(c1, c2, c3);
            context.SaveChanges();
        }
    }
}