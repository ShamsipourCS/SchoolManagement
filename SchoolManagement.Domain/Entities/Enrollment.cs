using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student's enrollment in a course
/// </summary>
public class Enrollment : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private Enrollment() { }

    /// <summary>
    /// Date when the student enrolled in the course
    /// </summary>
    public DateTime EnrollDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    public Grade? Grade { get; private set; }

    /// <summary>
    /// Foreign key to the student profile
    /// </summary>
    public Guid StudentProfileId { get; private set; }

    /// <summary>
    /// Navigation property to the student profile
    /// </summary>
    public virtual StudentProfile StudentProfile { get; private set; } = null!;

    /// <summary>
    /// Foreign key to the course
    /// </summary>
    public Guid CourseId { get; private set; }

    /// <summary>
    /// Navigation property to the course
    /// </summary>
    public virtual Course Course { get; private set; } = null!;

    /// <summary>
    /// Factory method to create a new enrollment with validation
    /// </summary>
    /// <param name="studentProfileId">ID of the student profile</param>
    /// <param name="courseId">ID of the course</param>
    /// <param name="enrollDate">Enrollment date (defaults to now)</param>
    /// <returns>A valid Enrollment instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static Enrollment Create(Guid studentProfileId, Guid courseId, DateTime? enrollDate = null)
    {
        if (studentProfileId == Guid.Empty)
            throw new ArgumentException("Student profile ID is required", nameof(studentProfileId));

        if (courseId == Guid.Empty)
            throw new ArgumentException("Course ID is required", nameof(courseId));

        var actualEnrollDate = enrollDate ?? DateTime.UtcNow;

        if (actualEnrollDate > DateTime.UtcNow)
            throw new ArgumentException("Enrollment date cannot be in the future", nameof(enrollDate));

        return new Enrollment
        {
            StudentProfileId = studentProfileId,
            CourseId = courseId,
            EnrollDate = actualEnrollDate,
            Grade = null
        };
    }

    /// <summary>
    /// Assigns or updates the grade for this enrollment
    /// </summary>
    /// <param name="gradeValue">Grade value (0-100)</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when grade is invalid</exception>
    public void AssignGrade(decimal gradeValue)
    {
        try
        {
            Grade = new Grade(gradeValue);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(nameof(gradeValue), $"Invalid grade: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes the grade from this enrollment
    /// </summary>
    public void RemoveGrade()
    {
        Grade = null;
    }
}