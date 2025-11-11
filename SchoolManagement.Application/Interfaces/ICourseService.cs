using SchoolManagement.Application.DTOs.Courses;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service interface for course business operations
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Get all courses asynchronously
    /// </summary>
    /// <returns>Collection of course response DTOs</returns>
    Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync();

    /// <summary>
    /// Get course by ID asynchronously
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>Course response DTO if found, null otherwise</returns>
    Task<CourseResponseDto?> GetCourseByIdAsync(int id);

    /// <summary>
    /// Get course with teacher and enrollment details asynchronously
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>Course response DTO with teacher and enrollment information if found, null otherwise</returns>
    Task<CourseResponseDto?> GetCourseWithDetailsAsync(int id);

    /// <summary>
    /// Get all courses taught by a specific teacher asynchronously
    /// </summary>
    /// <param name="teacherId">Teacher identifier</param>
    /// <returns>Collection of course response DTOs for the teacher</returns>
    Task<IEnumerable<CourseResponseDto>> GetCoursesByTeacherIdAsync(int teacherId);

    /// <summary>
    /// Create a new course asynchronously
    /// </summary>
    /// <param name="courseCreateDto">Course creation data</param>
    /// <returns>Created course response DTO</returns>
    Task<CourseResponseDto> CreateCourseAsync(CourseCreateDto courseCreateDto);

    /// <summary>
    /// Update an existing course asynchronously
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <param name="courseUpdateDto">Course update data</param>
    /// <returns>Updated course response DTO if found, null otherwise</returns>
    Task<CourseResponseDto?> UpdateCourseAsync(int id, CourseUpdateDto courseUpdateDto);

    /// <summary>
    /// Delete a course asynchronously
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>True if deleted successfully, false if course not found or has active enrollments</returns>
    Task<bool> DeleteCourseAsync(int id);

    /// <summary>
    /// Check if a course exists asynchronously
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>True if course exists, false otherwise</returns>
    Task<bool> CourseExistsAsync(int id);
}
