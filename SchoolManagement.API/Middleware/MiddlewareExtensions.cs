namespace SchoolManagement.API.Middleware;

/// <summary>
/// Extension methods for registering custom middleware
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds request/response logging middleware to the application pipeline
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}
