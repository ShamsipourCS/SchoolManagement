using SchoolManagement.Application.DTOs.Students;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service interface for student business operations
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Get all students asynchronously
    /// </summary>
    /// <returns>Collection of student response DTOs</returns>
    Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();

    /// <summary>
    /// Get student by ID asynchronously
    /// </summary>
    /// <param name="id">Student profile identifier</param>
    /// <returns>Student response DTO if found, null otherwise</returns>
    Task<StudentResponseDto?> GetStudentByIdAsync(Guid id);

    /// <summary>
    /// Get student with enrollment details asynchronously
    /// </summary>
    /// <param name="id">Student profile identifier</param>
    /// <returns>Student response DTO with enrollment information if found, null otherwise</returns>
    Task<StudentResponseDto?> GetStudentWithEnrollmentsAsync(Guid id);

    /// <summary>
    /// Get all active students asynchronously
    /// </summary>
    /// <returns>Collection of active student response DTOs</returns>
    Task<IEnumerable<StudentResponseDto>> GetActiveStudentsAsync();

    /// <summary>
    /// Create a new student asynchronously
    /// </summary>
    /// <param name="studentCreateDto">Student creation data</param>
    /// <returns>Created student response DTO</returns>
    Task<StudentResponseDto> CreateStudentAsync(StudentCreateDto studentCreateDto);

    /// <summary>
    /// Update an existing student asynchronously
    /// </summary>
    /// <param name="id">Student profile identifier</param>
    /// <param name="studentUpdateDto">Student update data</param>
    /// <returns>Updated student response DTO if found, null otherwise</returns>
    Task<StudentResponseDto?> UpdateStudentAsync(Guid id, StudentUpdateDto studentUpdateDto);

    /// <summary>
    /// Delete a student asynchronously
    /// </summary>
    /// <param name="id">Student profile identifier</param>
    /// <returns>True if deleted successfully, false if student not found</returns>
    Task<bool> DeleteStudentAsync(Guid id);

    /// <summary>
    /// Check if a student exists asynchronously
    /// </summary>
    /// <param name="id">Student profile identifier</param>
    /// <returns>True if student exists, false otherwise</returns>
    Task<bool> StudentExistsAsync(Guid id);
}
