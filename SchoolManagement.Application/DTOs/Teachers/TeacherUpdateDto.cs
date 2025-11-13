using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for updating an existing teacher profile and user info
/// </summary>
public class TeacherUpdateDto
{
    /// <summary>
    /// Email address (updates User.Email)
    /// </summary>
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    public string? Email { get; set; }

    /// <summary>
    /// Full name of the teacher
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime? HireDate { get; set; }

    /// <summary>
    /// Indicates whether the user account is active
    /// </summary>
    public bool? IsActive { get; set; }
}
