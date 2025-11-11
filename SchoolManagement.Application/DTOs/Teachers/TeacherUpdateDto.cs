using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for updating an existing teacher
/// </summary>
public class TeacherUpdateDto
{
    /// <summary>
    /// Full name of the teacher
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the teacher (must be unique)
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    [Required(ErrorMessage = "Hire date is required")]
    public DateTime HireDate { get; set; }
}
