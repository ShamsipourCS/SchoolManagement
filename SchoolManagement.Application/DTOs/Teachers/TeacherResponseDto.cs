namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for teacher profile response (includes user info)
/// </summary>
public class TeacherResponseDto
{
    /// <summary>
    /// Teacher profile ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User ID (for authentication reference)
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Username for authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Indicates whether the user account is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Number of courses taught by this teacher
    /// </summary>
    public int CourseCount { get; set; }

    /// <summary>
    /// Date when the profile was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
