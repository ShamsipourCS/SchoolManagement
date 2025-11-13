using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for Enrollment entity
/// </summary>
public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        // Table name
        builder.ToTable("Enrollments");

        // Primary key
        builder.HasKey(e => e.Id);

        // EnrollDate configuration
        builder.Property(e => e.EnrollDate)
            .IsRequired();

        // StudentProfileId foreign key
        builder.Property(e => e.StudentProfileId)
            .IsRequired();

        // CourseId foreign key
        builder.Property(e => e.CourseId)
            .IsRequired();

        // Configure Grade as owned value object (nullable)
        builder.OwnsOne(e => e.Grade, grade =>
        {
            grade.Property(g => g.Value)
                .HasColumnName("Grade")
                .HasColumnType("decimal(5,2)");
        });

        // Audit fields
        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // Relationships - already configured in StudentProfileConfiguration
        builder.HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Composite index to prevent duplicate enrollments
        builder.HasIndex(e => new { e.StudentProfileId, e.CourseId })
            .IsUnique(false)
            .HasDatabaseName("IX_Enrollments_StudentProfileId_CourseId");
    }
}
