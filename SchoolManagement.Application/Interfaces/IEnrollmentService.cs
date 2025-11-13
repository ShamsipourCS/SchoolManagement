using SchoolManagement.Application.DTOs.Enrollments;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Service interface for enrollment business operations
/// </summary>
public interface IEnrollmentService
{
    /// <summary>
    /// Get all enrollments asynchronously
    /// </summary>
    /// <returns>Collection of enrollment response DTOs</returns>
    Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync();

    /// <summary>
    /// Get enrollment by ID asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>Enrollment response DTO if found, null otherwise</returns>
    Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(Guid id);

    /// <summary>
    /// Get enrollment with student and course details asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>Enrollment response DTO with student and course information if found, null otherwise</returns>
    Task<EnrollmentResponseDto?> GetEnrollmentWithDetailsAsync(Guid id);

    /// <summary>
    /// Get all enrollments for a specific student asynchronously
    /// </summary>
    /// <param name="studentId">Student identifier</param>
    /// <returns>Collection of enrollment response DTOs for the student</returns>
    Task<IEnumerable<EnrollmentResponseDto>> GetEnrollmentsByStudentIdAsync(int studentId);

    /// <summary>
    /// Get all enrollments for a specific course asynchronously
    /// </summary>
    /// <param name="courseId">Course identifier</param>
    /// <returns>Collection of enrollment response DTOs for the course</returns>
    Task<IEnumerable<EnrollmentResponseDto>> GetEnrollmentsByCourseIdAsync(int courseId);

    /// <summary>
    /// Create a new enrollment asynchronously
    /// </summary>
    /// <param name="enrollmentCreateDto">Enrollment creation data</param>
    /// <returns>Created enrollment response DTO</returns>
    Task<EnrollmentResponseDto> CreateEnrollmentAsync(EnrollmentCreateDto enrollmentCreateDto);

    /// <summary>
    /// Update an existing enrollment asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <param name="enrollmentUpdateDto">Enrollment update data</param>
    /// <returns>Updated enrollment response DTO if found, null otherwise</returns>
    Task<EnrollmentResponseDto?> UpdateEnrollmentAsync(Guid id, EnrollmentUpdateDto enrollmentUpdateDto);

    /// <summary>
    /// Update the grade for an enrollment asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <param name="grade">Grade value (0-100)</param>
    /// <returns>Updated enrollment response DTO if found and grade is valid, null otherwise</returns>
    Task<EnrollmentResponseDto?> UpdateEnrollmentGradeAsync(Guid id, decimal grade);

    /// <summary>
    /// Delete an enrollment asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>True if deleted successfully, false if enrollment not found</returns>
    Task<bool> DeleteEnrollmentAsync(Guid id);

    /// <summary>
    /// Check if an enrollment exists asynchronously
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>True if enrollment exists, false otherwise</returns>
    Task<bool> EnrollmentExistsAsync(Guid id);

    /// <summary>
    /// Check if a student is already enrolled in a course asynchronously
    /// </summary>
    /// <param name="studentId">Student identifier</param>
    /// <param name="courseId">Course identifier</param>
    /// <returns>True if student is already enrolled in the course, false otherwise</returns>
    Task<bool> IsStudentEnrolledAsync(int studentId, int courseId);
}
