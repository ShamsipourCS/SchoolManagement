namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for teacher response
/// </summary>
public class TeacherResponseDto
{
    /// <summary>
    /// Teacher ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the teacher
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Number of courses taught by this teacher
    /// </summary>
    public int CourseCount { get; set; }

    /// <summary>
    /// Date when the teacher record was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
