using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Enrollments;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// API controller for enrollment management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly ILogger<EnrollmentsController> _logger;

    /// <summary>
    /// Initializes a new instance of the EnrollmentsController
    /// </summary>
    /// <param name="enrollmentService">Enrollment service for business operations</param>
    /// <param name="logger">Logger instance</param>
    public EnrollmentsController(IEnrollmentService enrollmentService, ILogger<EnrollmentsController> logger)
    {
        _enrollmentService = enrollmentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all enrollments with optional filtering
    /// </summary>
    /// <param name="studentId">Optional filter by student ID</param>
    /// <param name="courseId">Optional filter by course ID</param>
    /// <returns>List of enrollments</returns>
    /// <response code="200">Returns the list of enrollments</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EnrollmentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetEnrollments(
        [FromQuery] int? studentId = null,
        [FromQuery] int? courseId = null)
    {
        _logger.LogInformation("Getting enrollments with filters - StudentId: {StudentId}, CourseId: {CourseId}",
            studentId, courseId);

        IEnumerable<EnrollmentResponseDto> enrollments;

        if (studentId.HasValue)
        {
            _logger.LogInformation("Filtering enrollments by student ID: {StudentId}", studentId.Value);
            enrollments = await _enrollmentService.GetEnrollmentsByStudentIdAsync(studentId.Value);
        }
        else if (courseId.HasValue)
        {
            _logger.LogInformation("Filtering enrollments by course ID: {CourseId}", courseId.Value);
            enrollments = await _enrollmentService.GetEnrollmentsByCourseIdAsync(courseId.Value);
        }
        else
        {
            _logger.LogInformation("Getting all enrollments");
            enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
        }

        return Ok(enrollments);
    }

    /// <summary>
    /// Get a specific enrollment by ID
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>Enrollment details</returns>
    /// <response code="200">Returns the enrollment</response>
    /// <response code="404">If the enrollment is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EnrollmentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDto>> GetEnrollment(int id)
    {
        _logger.LogInformation("Getting enrollment with ID: {EnrollmentId}", id);

        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

        if (enrollment == null)
        {
            _logger.LogWarning("Enrollment with ID {EnrollmentId} not found", id);
            return NotFound(new { message = $"Enrollment with ID {id} not found" });
        }

        return Ok(enrollment);
    }

    /// <summary>
    /// Get an enrollment with student and course details
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>Enrollment with complete details</returns>
    /// <response code="200">Returns the enrollment with details</response>
    /// <response code="404">If the enrollment is not found</response>
    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(EnrollmentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDto>> GetEnrollmentWithDetails(int id)
    {
        _logger.LogInformation("Getting enrollment with ID: {EnrollmentId} including details", id);

        var enrollment = await _enrollmentService.GetEnrollmentWithDetailsAsync(id);

        if (enrollment == null)
        {
            _logger.LogWarning("Enrollment with ID {EnrollmentId} not found", id);
            return NotFound(new { message = $"Enrollment with ID {id} not found" });
        }

        return Ok(enrollment);
    }

    /// <summary>
    /// Get all enrollments for a specific student
    /// </summary>
    /// <param name="studentId">Student identifier</param>
    /// <returns>List of student's enrollments</returns>
    /// <response code="200">Returns the list of enrollments</response>
    [HttpGet("student/{studentId}")]
    [ProducesResponseType(typeof(IEnumerable<EnrollmentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetEnrollmentsByStudent(int studentId)
    {
        _logger.LogInformation("Getting enrollments for student with ID: {StudentId}", studentId);
        var enrollments = await _enrollmentService.GetEnrollmentsByStudentIdAsync(studentId);
        return Ok(enrollments);
    }

    /// <summary>
    /// Get all enrollments for a specific course
    /// </summary>
    /// <param name="courseId">Course identifier</param>
    /// <returns>List of course enrollments</returns>
    /// <response code="200">Returns the list of enrollments</response>
    [HttpGet("course/{courseId}")]
    [ProducesResponseType(typeof(IEnumerable<EnrollmentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetEnrollmentsByCourse(int courseId)
    {
        _logger.LogInformation("Getting enrollments for course with ID: {CourseId}", courseId);
        var enrollments = await _enrollmentService.GetEnrollmentsByCourseIdAsync(courseId);
        return Ok(enrollments);
    }

    /// <summary>
    /// Create a new enrollment
    /// </summary>
    /// <param name="enrollmentCreateDto">Enrollment creation data</param>
    /// <returns>The newly created enrollment</returns>
    /// <response code="201">Returns the newly created enrollment</response>
    /// <response code="400">If the request is invalid, student/course doesn't exist, or duplicate enrollment</response>
    [HttpPost]
    [ProducesResponseType(typeof(EnrollmentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EnrollmentResponseDto>> CreateEnrollment([FromBody] EnrollmentCreateDto enrollmentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for enrollment creation");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new enrollment for student {StudentId} in course {CourseId}",
            enrollmentCreateDto.StudentId, enrollmentCreateDto.CourseId);

        try
        {
            var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentCreateDto);

            _logger.LogInformation("Enrollment created successfully with ID: {EnrollmentId}", createdEnrollment.Id);

            return CreatedAtAction(
                nameof(GetEnrollment),
                new { id = createdEnrollment.Id },
                createdEnrollment);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to create enrollment: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing enrollment
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <param name="enrollmentUpdateDto">Enrollment update data</param>
    /// <returns>The updated enrollment</returns>
    /// <response code="200">Returns the updated enrollment</response>
    /// <response code="400">If the request is invalid or grade is out of range</response>
    /// <response code="404">If the enrollment is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EnrollmentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDto>> UpdateEnrollment(int id, [FromBody] EnrollmentUpdateDto enrollmentUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for enrollment update");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating enrollment with ID: {EnrollmentId}", id);

        try
        {
            var updatedEnrollment = await _enrollmentService.UpdateEnrollmentAsync(id, enrollmentUpdateDto);

            if (updatedEnrollment == null)
            {
                _logger.LogWarning("Enrollment with ID {EnrollmentId} not found for update", id);
                return NotFound(new { message = $"Enrollment with ID {id} not found" });
            }

            _logger.LogInformation("Enrollment with ID {EnrollmentId} updated successfully", id);

            return Ok(updatedEnrollment);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Failed to update enrollment: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update the grade for an enrollment
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <param name="gradeDto">Grade update data</param>
    /// <returns>The updated enrollment</returns>
    /// <response code="200">Returns the updated enrollment</response>
    /// <response code="400">If the grade is invalid (not between 0-100)</response>
    /// <response code="404">If the enrollment is not found</response>
    [HttpPatch("{id}/grade")]
    [ProducesResponseType(typeof(EnrollmentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDto>> UpdateEnrollmentGrade(int id, [FromBody] GradeUpdateDto gradeDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for grade update");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating grade for enrollment with ID: {EnrollmentId}", id);

        try
        {
            var updatedEnrollment = await _enrollmentService.UpdateEnrollmentGradeAsync(id, gradeDto.Grade);

            if (updatedEnrollment == null)
            {
                _logger.LogWarning("Enrollment with ID {EnrollmentId} not found for grade update", id);
                return NotFound(new { message = $"Enrollment with ID {id} not found" });
            }

            _logger.LogInformation("Grade updated successfully for enrollment with ID {EnrollmentId}", id);

            return Ok(updatedEnrollment);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Failed to update grade: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete an enrollment
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">If the enrollment was deleted successfully</response>
    /// <response code="404">If the enrollment is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        _logger.LogInformation("Deleting enrollment with ID: {EnrollmentId}", id);

        var result = await _enrollmentService.DeleteEnrollmentAsync(id);

        if (!result)
        {
            _logger.LogWarning("Enrollment with ID {EnrollmentId} not found for deletion", id);
            return NotFound(new { message = $"Enrollment with ID {id} not found" });
        }

        _logger.LogInformation("Enrollment with ID {EnrollmentId} deleted successfully", id);

        return NoContent();
    }

    /// <summary>
    /// Check if an enrollment exists
    /// </summary>
    /// <param name="id">Enrollment identifier</param>
    /// <returns>Boolean indicating existence</returns>
    /// <response code="200">Returns true if enrollment exists, false otherwise</response>
    [HttpGet("{id}/exists")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> EnrollmentExists(int id)
    {
        _logger.LogInformation("Checking if enrollment with ID {EnrollmentId} exists", id);
        var exists = await _enrollmentService.EnrollmentExistsAsync(id);
        return Ok(exists);
    }

    /// <summary>
    /// Check if a student is already enrolled in a course
    /// </summary>
    /// <param name="studentId">Student identifier</param>
    /// <param name="courseId">Course identifier</param>
    /// <returns>Boolean indicating if student is enrolled</returns>
    /// <response code="200">Returns true if student is enrolled in course, false otherwise</response>
    [HttpGet("check-enrollment")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> IsStudentEnrolled([FromQuery] int studentId, [FromQuery] int courseId)
    {
        _logger.LogInformation("Checking if student {StudentId} is enrolled in course {CourseId}", studentId, courseId);
        var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(studentId, courseId);
        return Ok(isEnrolled);
    }
}

/// <summary>
/// DTO for grade update operations
/// </summary>
public class GradeUpdateDto
{
    /// <summary>
    /// Grade value (0-100)
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
    public decimal Grade { get; set; }
}
