using SchoolManagement.Infrastructure.Extensions;
using SchoolManagement.Application.Extensions;
using SchoolManagement.API.Extensions;
using Serilog;
using Serilog.Events;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/schoolmanagement-.log",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 10_485_760, // 10 MB
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [{MachineName}] [{ThreadId}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting School Management API");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Register infrastructure services (DbContext, repositories, UnitOfWork)
    builder.Services.AddInfrastructure(builder.Configuration);

    // Register application services (AutoMapper and business services)
    builder.Services.AddApplicationServices();

    // Configure CORS policy
    var corsAllowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    var corsAllowedMethods = builder.Configuration.GetSection("Cors:AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
    var corsAllowedHeaders = builder.Configuration.GetSection("Cors:AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();
    var corsAllowCredentials = builder.Configuration.GetValue<bool>("Cors:AllowCredentials");
    var corsMaxAge = builder.Configuration.GetValue<int>("Cors:MaxAge");

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("SchoolManagementPolicy", policy =>
        {
            policy.WithOrigins(corsAllowedOrigins)
                  .WithMethods(corsAllowedMethods)
                  .WithHeaders(corsAllowedHeaders)
                  .SetPreflightMaxAge(TimeSpan.FromSeconds(corsMaxAge));

            if (corsAllowCredentials)
            {
                policy.AllowCredentials();
            }
        });
    });

    builder.Services.AddControllers();

    // Configure Swagger/OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "School Management API",
            Description =
                "An ASP.NET Core Web API for managing school data including students, teachers, courses, and enrollments",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "School Management Team"
            }
        });

        // Include XML comments for better API documentation
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (System.IO.File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline

    // Global exception handling (must be first in pipeline)
    app.UseGlobalExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management API v1");
            options.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();

    // Enable CORS
    app.UseCors("SchoolManagementPolicy");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}