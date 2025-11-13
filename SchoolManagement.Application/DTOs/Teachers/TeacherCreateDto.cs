using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Teachers;

/// <summary>
/// DTO for creating a new teacher (creates both User and TeacherProfile)
/// </summary>
public class TeacherCreateDto
{
    /// <summary>
    /// User ID for authentication
    /// </summary>
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    /// <summary>
    /// Full name of the teacher
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    [Required(ErrorMessage = "Hire date is required")]
    public DateTime HireDate { get; set; }
}