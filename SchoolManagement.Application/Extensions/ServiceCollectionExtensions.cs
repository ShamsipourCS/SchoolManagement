using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Services;

namespace SchoolManagement.Application.Extensions;

/// <summary>
/// Extension methods for configuring application services in the dependency injection container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all application layer services including AutoMapper and business services
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <param name="configuration">Application configuration</param>
    /// <returns>The configured service collection for method chaining</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register AutoMapper with all profiles from this assembly
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Configure JWT settings from appsettings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Register JWT token service with scoped lifetime
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Register application services with scoped lifetime
        // Scoped lifetime ensures services are created once per request in web applications
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
