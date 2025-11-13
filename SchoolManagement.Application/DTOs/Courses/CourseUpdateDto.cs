using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Courses;

/// <summary>
/// DTO for updating an existing course
/// </summary>
public class CourseUpdateDto
{
    /// <summary>
    /// Title of the course
    /// </summary>
    [Required(ErrorMessage = "Course title is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Course title must be between 2 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the course
    /// </summary>
    [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters")]
    public string? Description { get; set; }

    /// <summary>
    /// Date when the course starts
    /// </summary>
    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Foreign key to the teacher profile teaching this course
    /// </summary>
    public int? TeacherProfileId { get; set; }
}