using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Enrollments;

/// <summary>
/// DTO for creating a new enrollment
/// </summary>
public class EnrollmentCreateDto
{
    /// <summary>
    /// Foreign key to the student
    /// </summary>
    [Required(ErrorMessage = "Student ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a valid positive number")]
    public int StudentId { get; set; }

    /// <summary>
    /// Foreign key to the course
    /// </summary>
    [Required(ErrorMessage = "Course ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a valid positive number")]
    public int CourseId { get; set; }

    /// <summary>
    /// Date when the student enrolled in the course (defaults to current date)
    /// </summary>
    public DateTime? EnrollDate { get; set; }

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
    public decimal? Grade { get; set; }
}
