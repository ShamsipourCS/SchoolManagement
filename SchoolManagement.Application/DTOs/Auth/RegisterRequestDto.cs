using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Auth;

/// <summary>
/// DTO for user registration request
/// </summary>
public class RegisterRequestDto
{
    /// <summary>
    /// Username for the new account
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email address for the new account
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password for the new account
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// User role (optional, defaults to "User")
    /// </summary>
    [StringLength(50, ErrorMessage = "Role must not exceed 50 characters")]
    public string Role { get; set; } = "User";
}
