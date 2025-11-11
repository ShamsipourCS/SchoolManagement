using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Enrollments;

/// <summary>
/// DTO for updating an existing enrollment
/// </summary>
public class EnrollmentUpdateDto
{
    /// <summary>
    /// Date when the student enrolled in the course
    /// </summary>
    [Required(ErrorMessage = "Enrollment date is required")]
    public DateTime EnrollDate { get; set; }

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
    public decimal? Grade { get; set; }
}
