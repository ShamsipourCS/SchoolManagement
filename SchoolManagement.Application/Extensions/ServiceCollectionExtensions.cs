using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Interfaces;
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
    /// <returns>The configured service collection for method chaining</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper with all profiles from this assembly
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Register application services with scoped lifetime
        // Scoped lifetime ensures services are created once per request in web applications
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}
