namespace SchoolManagement.API.Models;

/// <summary>
/// Standard error response model for API exceptions
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Error message for the user
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detailed error information (only in development)
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Unique identifier for the error instance
    /// </summary>
    public string TraceId { get; set; } = string.Empty;

    /// <summary>
    /// Path where the error occurred
    /// </summary>
    public string? Path { get; set; }
}
