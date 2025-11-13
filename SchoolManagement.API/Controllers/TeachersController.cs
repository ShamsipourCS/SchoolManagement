using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Teachers;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// API controller for teacher management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly ILogger<TeachersController> _logger;

    /// <summary>
    /// Initializes a new instance of the TeachersController
    /// </summary>
    /// <param name="teacherService">Teacher service for business operations</param>
    /// <param name="logger">Logger instance</param>
    public TeachersController(ITeacherService teacherService, ILogger<TeachersController> logger)
    {
        _teacherService = teacherService;
        _logger = logger;
    }

    /// <summary>
    /// Get all teachers
    /// </summary>
    /// <returns>List of all teachers</returns>
    /// <response code="200">Returns the list of teachers</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TeacherResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetAllTeachers()
    {
        _logger.LogInformation("Getting all teachers");
        var teachers = await _teacherService.GetAllTeachersAsync();
        return Ok(teachers);
    }

    /// <summary>
    /// Get a specific teacher by ID
    /// </summary>
    /// <param name="id">Teacher identifier</param>
    /// <returns>Teacher details</returns>
    /// <response code="200">Returns the teacher</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> GetTeacher(int id)
    {
        _logger.LogInformation("Getting teacher with ID: {TeacherId}", id);

        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        if (teacher == null)
        {
            _logger.LogWarning("Teacher with ID {TeacherId} not found", id);
            return NotFound(new { message = $"Teacher with ID {id} not found" });
        }

        return Ok(teacher);
    }

    /// <summary>
    /// Get a teacher with all course details
    /// </summary>
    /// <param name="id">Teacher identifier</param>
    /// <returns>Teacher with course details</returns>
    /// <response code="200">Returns the teacher with courses</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpGet("{id}/courses")]
    [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> GetTeacherWithCourses(int id)
    {
        _logger.LogInformation("Getting teacher with ID: {TeacherId} including courses", id);

        var teacher = await _teacherService.GetTeacherWithCoursesAsync(id);

        if (teacher == null)
        {
            _logger.LogWarning("Teacher with ID {TeacherId} not found", id);
            return NotFound(new { message = $"Teacher with ID {id} not found" });
        }

        return Ok(teacher);
    }

    /// <summary>
    /// Get a teacher by email address
    /// </summary>
    /// <param name="email">Teacher email address</param>
    /// <returns>Teacher details</returns>
    /// <response code="200">Returns the teacher</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpGet("by-email/{email}")]
    [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> GetTeacherByEmail(string email)
    {
        _logger.LogInformation("Getting teacher with email: {Email}", email);

        var teacher = await _teacherService.GetTeacherByEmailAsync(email);

        if (teacher == null)
        {
            _logger.LogWarning("Teacher with email {Email} not found", email);
            return NotFound(new { message = $"Teacher with email {email} not found" });
        }

        return Ok(teacher);
    }

    /// <summary>
    /// Create a new teacher
    /// </summary>
    /// <param name="teacherCreateDto">Teacher creation data</param>
    /// <returns>The newly created teacher</returns>
    /// <response code="201">Returns the newly created teacher</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TeacherResponseDto>> CreateTeacher([FromBody] TeacherCreateDto teacherCreateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for teacher creation");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new teacher: {TeacherName}", teacherCreateDto.FullName);

        try
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacherCreateDto);

            _logger.LogInformation("Teacher created successfully with ID: {TeacherId}", createdTeacher.Id);

            return CreatedAtAction(
                nameof(GetTeacher),
                new { id = createdTeacher.Id },
                createdTeacher);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to create teacher: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing teacher
    /// </summary>
    /// <param name="id">Teacher identifier</param>
    /// <param name="teacherUpdateDto">Teacher update data</param>
    /// <returns>The updated teacher</returns>
    /// <response code="200">Returns the updated teacher</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> UpdateTeacher(int id, [FromBody] TeacherUpdateDto teacherUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for teacher update");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating teacher with ID: {TeacherId}", id);

        try
        {
            var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, teacherUpdateDto);

            if (updatedTeacher == null)
            {
                _logger.LogWarning("Teacher with ID {TeacherId} not found for update", id);
                return NotFound(new { message = $"Teacher with ID {id} not found" });
            }

            _logger.LogInformation("Teacher with ID {TeacherId} updated successfully", id);

            return Ok(updatedTeacher);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to update teacher: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a teacher
    /// </summary>
    /// <param name="id">Teacher identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">If the teacher was deleted successfully</response>
    /// <response code="404">If the teacher is not found</response>
    /// <response code="400">If the teacher has assigned courses</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        _logger.LogInformation("Deleting teacher with ID: {TeacherId}", id);

        try
        {
            var result = await _teacherService.DeleteTeacherAsync(id);

            if (!result)
            {
                _logger.LogWarning("Teacher with ID {TeacherId} not found for deletion", id);
                return NotFound(new { message = $"Teacher with ID {id} not found" });
            }

            _logger.LogInformation("Teacher with ID {TeacherId} deleted successfully", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to delete teacher: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Check if a teacher exists
    /// </summary>
    /// <param name="id">Teacher identifier</param>
    /// <returns>Boolean indicating existence</returns>
    /// <response code="200">Returns true if teacher exists, false otherwise</response>
    [HttpGet("{id}/exists")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> TeacherExists(int id)
    {
        _logger.LogInformation("Checking if teacher with ID {TeacherId} exists", id);
        var exists = await _teacherService.TeacherExistsAsync(id);
        return Ok(exists);
    }

    /// <summary>
    /// Check if an email is already in use
    /// </summary>
    /// <param name="email">Email address to check</param>
    /// <returns>Boolean indicating if email exists</returns>
    /// <response code="200">Returns true if email exists, false otherwise</response>
    [HttpGet("email-exists/{email}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> EmailExists(string email)
    {
        _logger.LogInformation("Checking if email {Email} exists", email);
        var exists = await _teacherService.EmailExistsAsync(email);
        return Ok(exists);
    }
}
