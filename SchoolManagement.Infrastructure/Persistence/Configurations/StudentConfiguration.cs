using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for StudentProfile entity
/// </summary>
public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        // Table name
        builder.ToTable("StudentProfiles");

        // Primary key
        builder.HasKey(sp => sp.Id);

        // UserId - Foreign key to User
        builder.Property(sp => sp.UserId)
            .IsRequired();

        builder.HasIndex(sp => sp.UserId)
            .IsUnique()
            .HasDatabaseName("IX_StudentProfiles_UserId");

        // FullName configuration
        builder.Property(sp => sp.FullName)
            .IsRequired()
            .HasMaxLength(200);

        // BirthDate configuration
        builder.Property(sp => sp.BirthDate)
            .IsRequired();

        // Audit fields
        builder.Property(sp => sp.CreatedAt)
            .IsRequired();

        builder.Property(sp => sp.UpdatedAt)
            .IsRequired(false);

        // Relationships
        builder.HasMany(sp => sp.Enrollments)
            .WithOne(e => e.StudentProfile)
            .HasForeignKey(e => e.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
