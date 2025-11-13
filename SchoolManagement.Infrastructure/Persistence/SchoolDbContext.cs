using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence.Configurations;

namespace SchoolManagement.Infrastructure.Persistence;

/// <summary>
/// Database context for the School Management system
/// </summary>
public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    /// <summary>
    /// Users for authentication and authorization
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Student profiles containing domain-specific student data
    /// </summary>
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

    /// <summary>
    /// Teacher profiles containing domain-specific teacher data
    /// </summary>
    public DbSet<TeacherProfile> TeacherProfiles => Set<TeacherProfile>();

    /// <summary>
    /// Courses taught by teachers
    /// </summary>
    public DbSet<Course> Courses => Set<Course>();

    /// <summary>
    /// Student enrollments in courses
    /// </summary>
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StudentProfileConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherProfileConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
    }
}
