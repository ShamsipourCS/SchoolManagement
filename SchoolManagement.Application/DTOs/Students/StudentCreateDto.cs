using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for creating a new student (creates both User and StudentProfile)
/// </summary>
public class StudentCreateDto
{
    /// <summary>
    /// ID of the student
    /// </summary>
    [Required(ErrorMessage = "Student Id is required")]
    public int StudentId { get; set; }

    /// <summary>
    /// Full name of the student
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }
}