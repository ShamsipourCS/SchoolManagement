namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for student response
/// </summary>
public class StudentResponseDto
{
    /// <summary>
    /// Student ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Number of enrollments
    /// </summary>
    public int EnrollmentCount { get; set; }

    /// <summary>
    /// Date when the student was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
