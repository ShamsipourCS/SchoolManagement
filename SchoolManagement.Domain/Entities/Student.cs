using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student in the school management system
/// </summary>
public class Student : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private Student() { }

    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Indicates whether the student is currently active
    /// </summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>
    /// Collection of enrollments for this student
    /// </summary>
    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    /// <summary>
    /// Factory method to create a new student with validation
    /// </summary>
    /// <param name="fullName">Student's full name</param>
    /// <param name="birthDate">Student's date of birth</param>
    /// <returns>A valid Student instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static Student Create(string fullName, DateTime birthDate)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        if (birthDate >= DateTime.UtcNow)
            throw new ArgumentException("Birth date must be in the past", nameof(birthDate));

        if (birthDate < DateTime.UtcNow.AddYears(-120))
            throw new ArgumentException("Birth date is not realistic", nameof(birthDate));

        return new Student
        {
            FullName = fullName.Trim(),
            BirthDate = birthDate,
            IsActive = true
        };
    }

    /// <summary>
    /// Updates the student's full name
    /// </summary>
    /// <param name="fullName">New full name</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        FullName = fullName.Trim();
    }

    /// <summary>
    /// Deactivates the student
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the student
    /// </summary>
    public void Activate()
    {
        IsActive = true;
    }
}