namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for student profile response (includes user info)
/// </summary>
public class StudentResponseDto
{
    /// <summary>
    /// Student profile ID
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
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the user account is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Number of enrollments
    /// </summary>
    public int EnrollmentCount { get; set; }

    /// <summary>
    /// Date when the profile was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
