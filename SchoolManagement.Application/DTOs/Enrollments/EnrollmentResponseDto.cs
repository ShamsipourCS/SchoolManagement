namespace SchoolManagement.Application.DTOs.Enrollments;

/// <summary>
/// DTO for enrollment response
/// </summary>
public class EnrollmentResponseDto
{
    /// <summary>
    /// Enrollment ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date when the student enrolled in the course
    /// </summary>
    public DateTime EnrollDate { get; set; }

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    public decimal? Grade { get; set; }

    /// <summary>
    /// Foreign key to the student
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Name of the student
    /// </summary>
    public string StudentName { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key to the course
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Title of the course
    /// </summary>
    public string CourseTitle { get; set; } = string.Empty;

    /// <summary>
    /// Date when the enrollment was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
