using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// API controller for student management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;

    /// <summary>
    /// Initializes a new instance of the StudentsController
    /// </summary>
    /// <param name="studentService">Student service for business operations</param>
    /// <param name="logger">Logger instance</param>
    public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all students
    /// </summary>
    /// <returns>List of all students</returns>
    /// <response code="200">Returns the list of students</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudents()
    {
        _logger.LogInformation("Getting all students");
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    /// <summary>
    /// Get all active students
    /// </summary>
    /// <returns>List of active students</returns>
    /// <response code="200">Returns the list of active students</response>
    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<StudentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetActiveStudents()
    {
        _logger.LogInformation("Getting all active students");
        var students = await _studentService.GetActiveStudentsAsync();
        return Ok(students);
    }

    /// <summary>
    /// Get a specific student by ID
    /// </summary>
    /// <param name="id">Student identifier</param>
    /// <returns>Student details</returns>
    /// <response code="200">Returns the student</response>
    /// <response code="404">If the student is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> GetStudent(int id)
    {
        _logger.LogInformation("Getting student with ID: {StudentId}", id);

        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            _logger.LogWarning("Student with ID {StudentId} not found", id);
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        return Ok(student);
    }

    /// <summary>
    /// Get a student with all enrollment details
    /// </summary>
    /// <param name="id">Student identifier</param>
    /// <returns>Student with enrollment details</returns>
    /// <response code="200">Returns the student with enrollments</response>
    /// <response code="404">If the student is not found</response>
    [HttpGet("{id}/enrollments")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> GetStudentWithEnrollments(int id)
    {
        _logger.LogInformation("Getting student with ID: {StudentId} including enrollments", id);

        var student = await _studentService.GetStudentWithEnrollmentsAsync(id);

        if (student == null)
        {
            _logger.LogWarning("Student with ID {StudentId} not found", id);
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        return Ok(student);
    }

    /// <summary>
    /// Create a new student
    /// </summary>
    /// <param name="studentCreateDto">Student creation data</param>
    /// <returns>The newly created student</returns>
    /// <response code="201">Returns the newly created student</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentResponseDto>> CreateStudent([FromBody] StudentCreateDto studentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for student creation");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new student: {StudentName}", studentCreateDto.FullName);

        var createdStudent = await _studentService.CreateStudentAsync(studentCreateDto);

        _logger.LogInformation("Student created successfully with ID: {StudentId}", createdStudent.Id);

        return CreatedAtAction(
            nameof(GetStudent),
            new { id = createdStudent.Id },
            createdStudent);
    }

    /// <summary>
    /// Update an existing student
    /// </summary>
    /// <param name="id">Student identifier</param>
    /// <param name="studentUpdateDto">Student update data</param>
    /// <returns>The updated student</returns>
    /// <response code="200">Returns the updated student</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the student is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> UpdateStudent(int id, [FromBody] StudentUpdateDto studentUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for student update");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating student with ID: {StudentId}", id);

        var updatedStudent = await _studentService.UpdateStudentAsync(id, studentUpdateDto);

        if (updatedStudent == null)
        {
            _logger.LogWarning("Student with ID {StudentId} not found for update", id);
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        _logger.LogInformation("Student with ID {StudentId} updated successfully", id);

        return Ok(updatedStudent);
    }

    /// <summary>
    /// Delete a student
    /// </summary>
    /// <param name="id">Student identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">If the student was deleted successfully</response>
    /// <response code="404">If the student is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        _logger.LogInformation("Deleting student with ID: {StudentId}", id);

        var result = await _studentService.DeleteStudentAsync(id);

        if (!result)
        {
            _logger.LogWarning("Student with ID {StudentId} not found for deletion", id);
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        _logger.LogInformation("Student with ID {StudentId} deleted successfully", id);

        return NoContent();
    }

    /// <summary>
    /// Check if a student exists
    /// </summary>
    /// <param name="id">Student identifier</param>
    /// <returns>Boolean indicating existence</returns>
    /// <response code="200">Returns true if student exists, false otherwise</response>
    [HttpGet("{id}/exists")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> StudentExists(int id)
    {
        _logger.LogInformation("Checking if student with ID {StudentId} exists", id);
        var exists = await _studentService.StudentExistsAsync(id);
        return Ok(exists);
    }
}
