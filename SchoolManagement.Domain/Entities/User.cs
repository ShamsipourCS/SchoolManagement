using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a user in the school management system for authentication and authorization
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Unique username for authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// BCrypt hashed password
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// User's role in the system (Admin, Teacher, Student)
    /// </summary>
    public string Role { get; set; } = UserRole.Student.ToString();

    /// <summary>
    /// Indicates whether the user account is active
    /// </summary>
    public bool IsActive { get; set; } = true;
}
