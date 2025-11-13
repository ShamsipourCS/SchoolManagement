using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for Course entity
/// </summary>
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Table name
        builder.ToTable("Courses");

        // Primary key
        builder.HasKey(c => c.Id);

        // Title configuration
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        // Description configuration
        builder.Property(c => c.Description)
            .HasMaxLength(2000);

        // StartDate configuration
        builder.Property(c => c.StartDate)
            .IsRequired();

        // TeacherProfileId foreign key
        builder.Property(c => c.TeacherProfileId)
            .IsRequired();

        // Audit fields
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);

        // Relationships - already configured in TeacherProfileConfiguration
    }
}
