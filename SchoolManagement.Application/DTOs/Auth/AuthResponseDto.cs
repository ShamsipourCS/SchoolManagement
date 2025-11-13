namespace SchoolManagement.Application.DTOs.Auth;

/// <summary>
/// DTO for authentication response
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// JWT token for authenticated requests
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Username of the authenticated user
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email of the authenticated user
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Role of the authenticated user
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
