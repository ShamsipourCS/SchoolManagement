using SchoolManagement.Infrastructure.Extensions;
using SchoolManagement.Application.Extensions;
using SchoolManagement.API.Extensions;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Persistence.Seed;

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
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [{MachineName}] [{ThreadId}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting School Management API");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Register infrastructure services (DbContext, repositories, UnitOfWork)
    builder.Services.AddInfrastructure(builder.Configuration);

    // Register application services (AutoMapper, JWT, and business services)
    builder.Services.AddApplicationServices(builder.Configuration);

    // Configure JWT Authentication
    var jwtSecret = builder.Configuration["JwtSettings:Secret"]
                    ?? throw new InvalidOperationException("JWT Secret is not configured");
    var jwtIssuer = builder.Configuration["JwtSettings:Issuer"]
                    ?? throw new InvalidOperationException("JWT Issuer is not configured");
    var jwtAudience = builder.Configuration["JwtSettings:Audience"]
                      ?? throw new InvalidOperationException("JWT Audience is not configured");

    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ClockSkew = TimeSpan.Zero // Remove default 5 minute tolerance
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Log.Warning("Authentication failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Log.Debug("Token validated for user: {User}", context.Principal?.Identity?.Name);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Log.Warning("Authentication challenge: {Error} - {ErrorDescription}",
                        context.Error, context.ErrorDescription);
                    return Task.CompletedTask;
                }
            };
        });

    // Configure Authorization policies
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
    });

    // Configure CORS policy
    var corsAllowedOrigins =
        builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    var corsAllowedMethods =
        builder.Configuration.GetSection("Cors:AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
    var corsAllowedHeaders =
        builder.Configuration.GetSection("Cors:AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();
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

        // Configure JWT Authentication for Swagger
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
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

    // ----- DEVELOPMENT SEED -----
    if (app.Environment.IsDevelopment())
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

            // Ensure database exists and migrations are applied
            context.Database.Migrate();

            // Seed development data
            DevelopmentSeed.Seed(context);
            // Or async version if you convert it to async:
            // await DevelopmentSeed.SeedAsync(context);
        }
    }

    // Configure the HTTP request pipeline

    // Global exception handling (must be first in pipeline)
    app.UseGlobalExceptionHandler();

    // Request/response logging (after exception handling)
    app.UseRequestLogging();

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

    // Enable authentication and authorization
    app.UseAuthentication();
    app.UseAuthorization();

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