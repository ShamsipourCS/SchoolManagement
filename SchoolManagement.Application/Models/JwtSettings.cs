namespace SchoolManagement.Application.Models;

/// <summary>
/// Configuration settings for JWT token generation and validation
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Secret key used for signing JWT tokens (minimum 32 characters recommended)
    /// </summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// Token issuer (typically the API application name)
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Token audience (typically the client application)
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Token expiration time in minutes (default: 60 minutes)
    /// </summary>
    public int ExpiryInMinutes { get; set; } = 60;
}
