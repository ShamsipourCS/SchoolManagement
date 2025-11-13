using SchoolManagement.Application.DTOs.Teachers;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service interface for teacher business operations
/// </summary>
public interface ITeacherService
{
    /// <summary>
    /// Get all teachers asynchronously
    /// </summary>
    /// <returns>Collection of teacher response DTOs</returns>
    Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync();

    /// <summary>
    /// Get teacher by ID asynchronously
    /// </summary>
    /// <param name="id">Teacher profile identifier</param>
    /// <returns>Teacher response DTO if found, null otherwise</returns>
    Task<TeacherResponseDto?> GetTeacherByIdAsync(int id);

    /// <summary>
    /// Get teacher with course details asynchronously
    /// </summary>
    /// <param name="id">Teacher profile identifier</param>
    /// <returns>Teacher response DTO with course information if found, null otherwise</returns>
    Task<TeacherResponseDto?> GetTeacherWithCoursesAsync(int id);

    /// <summary>
    /// Get teacher by email address asynchronously
    /// </summary>
    /// <param name="email">Teacher email address</param>
    /// <returns>Teacher response DTO if found, null otherwise</returns>
    Task<TeacherResponseDto?> GetTeacherByEmailAsync(string email);

    /// <summary>
    /// Create a new teacher asynchronously
    /// </summary>
    /// <param name="teacherCreateDto">Teacher creation data</param>
    /// <returns>Created teacher response DTO</returns>
    Task<TeacherResponseDto> CreateTeacherAsync(TeacherCreateDto teacherCreateDto);

    /// <summary>
    /// Update an existing teacher asynchronously
    /// </summary>
    /// <param name="id">Teacher profile identifier</param>
    /// <param name="teacherUpdateDto">Teacher update data</param>
    /// <returns>Updated teacher response DTO if found, null otherwise</returns>
    Task<TeacherResponseDto?> UpdateTeacherAsync(int id, TeacherUpdateDto teacherUpdateDto);

    /// <summary>
    /// Delete a teacher asynchronously
    /// </summary>
    /// <param name="id">Teacher profile identifier</param>
    /// <returns>True if deleted successfully, false if teacher not found or has assigned courses</returns>
    Task<bool> DeleteTeacherAsync(int id);

    /// <summary>
    /// Check if a teacher exists asynchronously
    /// </summary>
    /// <param name="id">Teacher profile identifier</param>
    /// <returns>True if teacher exists, false otherwise</returns>
    Task<bool> TeacherExistsAsync(int id);

    /// <summary>
    /// Check if email is already in use by another user asynchronously
    /// </summary>
    /// <param name="email">Email address to check</param>
    /// <param name="excludeTeacherId">Optional teacher profile ID to exclude from check (for updates)</param>
    /// <returns>True if email exists, false otherwise</returns>
    Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null);
}