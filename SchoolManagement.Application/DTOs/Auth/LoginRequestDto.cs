using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Auth;

/// <summary>
/// DTO for user login request
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Username for authentication
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password for authentication
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
