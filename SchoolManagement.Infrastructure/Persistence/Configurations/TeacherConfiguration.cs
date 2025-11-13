using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for TeacherProfile entity
/// </summary>
public class TeacherProfileConfiguration : IEntityTypeConfiguration<TeacherProfile>
{
    public void Configure(EntityTypeBuilder<TeacherProfile> builder)
    {
        // Table name
        builder.ToTable("TeacherProfiles");

        // Primary key
        builder.HasKey(tp => tp.Id);

        // UserId - Foreign key to User
        builder.Property(tp => tp.UserId)
            .IsRequired();

        builder.HasIndex(tp => tp.UserId)
            .IsUnique()
            .HasDatabaseName("IX_TeacherProfiles_UserId");

        // FullName configuration
        builder.Property(tp => tp.FullName)
            .IsRequired()
            .HasMaxLength(200);

        // HireDate configuration
        builder.Property(tp => tp.HireDate)
            .IsRequired();

        // Audit fields
        builder.Property(tp => tp.CreatedAt)
            .IsRequired();

        builder.Property(tp => tp.UpdatedAt)
            .IsRequired(false);

        // Relationships
        builder.HasMany(tp => tp.Courses)
            .WithOne(c => c.TeacherProfile)
            .HasForeignKey(c => c.TeacherProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
