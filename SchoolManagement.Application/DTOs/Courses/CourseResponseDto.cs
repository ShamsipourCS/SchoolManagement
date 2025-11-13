namespace SchoolManagement.Application.DTOs.Courses;

/// <summary>
/// DTO for course response
/// </summary>
public class CourseResponseDto
{
    /// <summary>
    /// Course ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Title of the course
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the course
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the course starts
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Foreign key to the teacher profile teaching this course
    /// </summary>
    public Guid TeacherProfileId { get; set; }

    /// <summary>
    /// Name of the teacher teaching this course
    /// </summary>
    public string TeacherName { get; set; } = string.Empty;

    /// <summary>
    /// Number of students enrolled in this course
    /// </summary>
    public int EnrollmentCount { get; set; }

    /// <summary>
    /// Date when the course was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
