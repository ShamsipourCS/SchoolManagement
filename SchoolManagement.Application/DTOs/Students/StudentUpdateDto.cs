using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for updating an existing student
/// </summary>
public class StudentUpdateDto
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is active
    /// </summary>
    public bool IsActive { get; set; }
}
