using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a teacher in the school management system
/// </summary>
public class Teacher : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private Teacher() { }

    /// <summary>
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    /// Email address of the teacher (must be unique)
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; private set; }

    /// <summary>
    /// Collection of courses taught by this teacher
    /// </summary>
    public ICollection<Course> Courses { get; private set; } = new List<Course>();

    /// <summary>
    /// Factory method to create a new teacher with validation
    /// </summary>
    /// <param name="fullName">Teacher's full name</param>
    /// <param name="email">Teacher's email address</param>
    /// <param name="hireDate">Date when teacher was hired</param>
    /// <returns>A valid Teacher instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static Teacher Create(string fullName, string email, DateTime hireDate)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        // Email validation is handled by the Email value object
        Email emailValueObject;
        try
        {
            emailValueObject = new Email(email);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Invalid email: {ex.Message}", nameof(email), ex);
        }

        if (hireDate > DateTime.UtcNow)
            throw new ArgumentException("Hire date cannot be in the future", nameof(hireDate));

        if (hireDate < DateTime.UtcNow.AddYears(-50))
            throw new ArgumentException("Hire date is not realistic", nameof(hireDate));

        return new Teacher
        {
            FullName = fullName.Trim(),
            Email = emailValueObject,
            HireDate = hireDate
        };
    }

    /// <summary>
    /// Updates the teacher's full name
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
    /// Updates the teacher's email address
    /// </summary>
    /// <param name="email">New email address</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void UpdateEmail(string email)
    {
        try
        {
            Email = new Email(email);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Invalid email: {ex.Message}", nameof(email), ex);
        }
    }
}