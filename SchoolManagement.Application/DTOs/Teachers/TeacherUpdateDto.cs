using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for updating an existing teacher profile and user info
/// </summary>
public class TeacherUpdateDto
{
    /// <summary>
    /// Full name of the teacher
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;
}