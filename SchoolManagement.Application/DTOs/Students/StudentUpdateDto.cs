using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for updating an existing student profile and user info
/// </summary>
public class StudentUpdateDto
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;
}