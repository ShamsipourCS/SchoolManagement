using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a course in the school management system
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private Course()
    {
    }

    /// <summary>
    /// Title of the course
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Detailed description of the course
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Date when the course starts
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    /// Foreign key to the teacher profile teaching this course
    /// </summary>
    public int TeacherProfileId { get; private set; }

    /// <summary>
    /// Navigation property to the teacher profile
    /// </summary>
    public virtual TeacherProfile TeacherProfile { get; private set; } = null!;

    /// <summary>
    /// Collection of enrollments for this course
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    /// <summary>
    /// Factory method to create a new course with validation
    /// </summary>
    /// <param name="title">Course title</param>
    /// <param name="teacherProfileId">ID of the assigned teacher profile</param>
    /// <param name="startDate">Course start date</param>
    /// <param name="description">Optional course description</param>
    /// <returns>A valid Course instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static Course Create(string title, int teacherProfileId, DateTime startDate, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Course title is required and cannot be empty", nameof(title));

        if (title.Length > 200)
            throw new ArgumentException("Course title cannot exceed 200 characters", nameof(title));

        if (teacherProfileId <= 0)
            throw new ArgumentException("Teacher profile ID must be a valid positive number", nameof(teacherProfileId));

        if (!string.IsNullOrEmpty(description) && description.Length > 2000)
            throw new ArgumentException("Course description cannot exceed 2000 characters", nameof(description));

        return new Course
        {
            Title = title.Trim(),
            TeacherProfileId = teacherProfileId,
            StartDate = startDate,
            Description = description?.Trim()
        };
    }

    /// <summary>
    /// Updates the course title
    /// </summary>
    /// <param name="title">New course title</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Course title is required and cannot be empty", nameof(title));

        if (title.Length > 200)
            throw new ArgumentException("Course title cannot exceed 200 characters", nameof(title));

        Title = title.Trim();
    }

    /// <summary>
    /// Updates the course description
    /// </summary>
    /// <param name="description">New course description</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void UpdateDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description) && description.Length > 2000)
            throw new ArgumentException("Course description cannot exceed 2000 characters", nameof(description));

        Description = description?.Trim();
    }

    /// <summary>
    /// Updates the course start date
    /// </summary>
    /// <param name="startDate">New start date</param>
    public void UpdateStartDate(DateTime startDate)
    {
        StartDate = startDate;
    }

    /// <summary>
    /// Assigns a new teacher to the course
    /// </summary>
    /// <param name="teacherProfileId">ID of the new teacher profile</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void AssignTeacher(int teacherProfileId)
    {
        if (teacherProfileId <= 0)
            throw new ArgumentException("Teacher profile ID must be a valid positive number", nameof(teacherProfileId));

        TeacherProfileId = teacherProfileId;
    }
}