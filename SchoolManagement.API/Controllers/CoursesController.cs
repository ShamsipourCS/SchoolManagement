using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Courses;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// API controller for course management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CoursesController> _logger;

    /// <summary>
    /// Initializes a new instance of the CoursesController
    /// </summary>
    /// <param name="courseService">Course service for business operations</param>
    /// <param name="logger">Logger instance</param>
    public CoursesController(ICourseService courseService, ILogger<CoursesController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    /// <summary>
    /// Get all courses
    /// </summary>
    /// <returns>List of all courses</returns>
    /// <response code="200">Returns the list of courses</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CourseResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAllCourses()
    {
        _logger.LogInformation("Getting all courses");
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    /// <summary>
    /// Get a specific course by ID
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>Course details</returns>
    /// <response code="200">Returns the course</response>
    /// <response code="404">If the course is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CourseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponseDto>> GetCourse(int id)
    {
        _logger.LogInformation("Getting course with ID: {CourseId}", id);

        var course = await _courseService.GetCourseByIdAsync(id);

        if (course == null)
        {
            _logger.LogWarning("Course with ID {CourseId} not found", id);
            return NotFound(new { message = $"Course with ID {id} not found" });
        }

        return Ok(course);
    }

    /// <summary>
    /// Get a course with teacher and enrollment details
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>Course with complete details</returns>
    /// <response code="200">Returns the course with details</response>
    /// <response code="404">If the course is not found</response>
    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(CourseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponseDto>> GetCourseWithDetails(int id)
    {
        _logger.LogInformation("Getting course with ID: {CourseId} including details", id);

        var course = await _courseService.GetCourseWithDetailsAsync(id);

        if (course == null)
        {
            _logger.LogWarning("Course with ID {CourseId} not found", id);
            return NotFound(new { message = $"Course with ID {id} not found" });
        }

        return Ok(course);
    }

    /// <summary>
    /// Get all courses taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">Teacher identifier</param>
    /// <returns>List of courses taught by the teacher</returns>
    /// <response code="200">Returns the list of courses</response>
    [HttpGet("by-teacher/{teacherId}")]
    [ProducesResponseType(typeof(IEnumerable<CourseResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetCoursesByTeacher(int teacherId)
    {
        _logger.LogInformation("Getting courses for teacher with ID: {TeacherId}", teacherId);
        var courses = await _courseService.GetCoursesByTeacherIdAsync(teacherId);
        return Ok(courses);
    }

    /// <summary>
    /// Create a new course
    /// </summary>
    /// <param name="courseCreateDto">Course creation data</param>
    /// <returns>The newly created course</returns>
    /// <response code="201">Returns the newly created course</response>
    /// <response code="400">If the request is invalid or teacher doesn't exist</response>
    [HttpPost]
    [ProducesResponseType(typeof(CourseResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CourseResponseDto>> CreateCourse([FromBody] CourseCreateDto courseCreateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for course creation");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new course: {CourseTitle}", courseCreateDto.Title);

        try
        {
            var createdCourse = await _courseService.CreateCourseAsync(courseCreateDto);

            _logger.LogInformation("Course created successfully with ID: {CourseId}", createdCourse.Id);

            return CreatedAtAction(
                nameof(GetCourse),
                new { id = createdCourse.Id },
                createdCourse);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to create course: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing course
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <param name="courseUpdateDto">Course update data</param>
    /// <returns>The updated course</returns>
    /// <response code="200">Returns the updated course</response>
    /// <response code="400">If the request is invalid or teacher doesn't exist</response>
    /// <response code="404">If the course is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CourseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponseDto>> UpdateCourse(int id, [FromBody] CourseUpdateDto courseUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for course update");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating course with ID: {CourseId}", id);

        try
        {
            var updatedCourse = await _courseService.UpdateCourseAsync(id, courseUpdateDto);

            if (updatedCourse == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found for update", id);
                return NotFound(new { message = $"Course with ID {id} not found" });
            }

            _logger.LogInformation("Course with ID {CourseId} updated successfully", id);

            return Ok(updatedCourse);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to update course: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a course
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">If the course was deleted successfully</response>
    /// <response code="404">If the course is not found</response>
    /// <response code="400">If the course has active enrollments</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        _logger.LogInformation("Deleting course with ID: {CourseId}", id);

        try
        {
            var result = await _courseService.DeleteCourseAsync(id);

            if (!result)
            {
                _logger.LogWarning("Course with ID {CourseId} not found for deletion", id);
                return NotFound(new { message = $"Course with ID {id} not found" });
            }

            _logger.LogInformation("Course with ID {CourseId} deleted successfully", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed to delete course: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Check if a course exists
    /// </summary>
    /// <param name="id">Course identifier</param>
    /// <returns>Boolean indicating existence</returns>
    /// <response code="200">Returns true if course exists, false otherwise</response>
    [HttpGet("{id}/exists")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> CourseExists(int id)
    {
        _logger.LogInformation("Checking if course with ID {CourseId} exists", id);
        var exists = await _courseService.CourseExistsAsync(id);
        return Ok(exists);
    }
}
