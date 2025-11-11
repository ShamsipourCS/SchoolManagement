using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.FullName).HasMaxLength(200).IsRequired();

        // Configure Email as owned value object
        builder.OwnsOne(t => t.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(200)
                .IsRequired();

            // Create unique index on the Email column
            email.HasIndex(e => e.Value).IsUnique();
        });
    }
}
