using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Extensions;
using SchoolManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services (AutoMapper and business services)
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
