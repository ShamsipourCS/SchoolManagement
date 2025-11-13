using AutoMapper;
using SchoolManagement.Application.DTOs.Enrollments;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Application.Services;

/// <summary>
/// Service implementation for enrollment business operations
/// </summary>
public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all enrollments asynchronously
    /// </summary>
    public async Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        var enrollments = await _unitOfWork.Enrollments.GetAllAsync();
        return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollments);
    }

    /// <summary>
    /// Get enrollment by ID asynchronously
    /// </summary>
    public async Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(int id)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
        return enrollment == null ? null : _mapper.Map<EnrollmentResponseDto>(enrollment);
    }

    /// <summary>
    /// Get enrollment with student and course details asynchronously
    /// </summary>
    public async Task<EnrollmentResponseDto?> GetEnrollmentWithDetailsAsync(int id)
    {
        var enrollment = await _unitOfWork.Enrollments.GetWithDetailsAsync(id);
        return enrollment == null ? null : _mapper.Map<EnrollmentResponseDto>(enrollment);
    }

    /// <summary>
    /// Get all enrollments for a specific student asynchronously
    /// </summary>
    public async Task<IEnumerable<EnrollmentResponseDto>> GetEnrollmentsByStudentIdAsync(int studentId)
    {
        var enrollments = await _unitOfWork.Enrollments.GetByStudentProfileIdAsync(studentId);
        return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollments);
    }

    /// <summary>
    /// Get all enrollments for a specific course asynchronously
    /// </summary>
    public async Task<IEnumerable<EnrollmentResponseDto>> GetEnrollmentsByCourseIdAsync(int courseId)
    {
        var enrollments = await _unitOfWork.Enrollments.GetByCourseIdAsync(courseId);
        return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollments);
    }

    /// <summary>
    /// Create a new enrollment asynchronously
    /// </summary>
    public async Task<EnrollmentResponseDto> CreateEnrollmentAsync(EnrollmentCreateDto enrollmentCreateDto)
    {
        // Validate that student exists
        var studentExists = await _unitOfWork.StudentProfiles.ExistsAsync(enrollmentCreateDto.StudentProfileId);
        if (!studentExists)
        {
            throw new ArgumentException($"Student with ID {enrollmentCreateDto.StudentProfileId} does not exist.",
                nameof(enrollmentCreateDto.StudentProfileId));
        }

        // Validate that course exists
        var courseExists = await _unitOfWork.Courses.ExistsAsync(enrollmentCreateDto.CourseId);
        if (!courseExists)
        {
            throw new ArgumentException($"Course with ID {enrollmentCreateDto.CourseId} does not exist.",
                nameof(enrollmentCreateDto.CourseId));
        }

        // Prevent duplicate enrollments (same student + course)
        var isAlreadyEnrolled = await _unitOfWork.Enrollments.IsEnrolledAsync(
            enrollmentCreateDto.StudentProfileId,
            enrollmentCreateDto.CourseId);

        if (isAlreadyEnrolled)
        {
            throw new InvalidOperationException(
                $"Student with ID {enrollmentCreateDto.StudentProfileId} is already enrolled in course with ID {enrollmentCreateDto.CourseId}.");
        }

        // Use domain factory method to create entity with validation
        var enrollment = Enrollment.Create(
            enrollmentCreateDto.StudentProfileId,
            enrollmentCreateDto.CourseId,
            enrollmentCreateDto.EnrollDate);

        // Add to repository
        await _unitOfWork.Enrollments.AddAsync(enrollment);
        await _unitOfWork.SaveChangesAsync();

        // Reload with student and course details for response
        var createdEnrollment = await _unitOfWork.Enrollments.GetWithDetailsAsync(enrollment.Id);

        // Return mapped response
        return _mapper.Map<EnrollmentResponseDto>(createdEnrollment);
    }

    /// <summary>
    /// Update an existing enrollment asynchronously
    /// </summary>
    public async Task<EnrollmentResponseDto?> UpdateEnrollmentAsync(int id, EnrollmentUpdateDto enrollmentUpdateDto)
    {
        // Check if enrollment exists
        var existingEnrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
        if (existingEnrollment == null)
        {
            return null;
        }

        // Use domain methods to update grade
        if (enrollmentUpdateDto.Grade.HasValue)
        {
            existingEnrollment.AssignGrade(enrollmentUpdateDto.Grade.Value);
        }
        else
        {
            existingEnrollment.RemoveGrade();
        }

        // Note: EnrollDate, StudentId, and CourseId cannot be updated
        // This is intentional - these are immutable after creation

        // Update repository
        _unitOfWork.Enrollments.Update(existingEnrollment);
        await _unitOfWork.SaveChangesAsync();

        // Reload with student and course details for response
        var updatedEnrollment = await _unitOfWork.Enrollments.GetWithDetailsAsync(id);

        // Return mapped response
        return _mapper.Map<EnrollmentResponseDto>(updatedEnrollment);
    }

    /// <summary>
    /// Update the grade for an enrollment asynchronously
    /// </summary>
    public async Task<EnrollmentResponseDto?> UpdateEnrollmentGradeAsync(int id, decimal grade)
    {
        // Check if enrollment exists
        var existingEnrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
        if (existingEnrollment == null)
        {
            return null;
        }

        // Use domain method to assign grade (includes validation)
        existingEnrollment.AssignGrade(grade);

        // Update repository
        _unitOfWork.Enrollments.Update(existingEnrollment);
        await _unitOfWork.SaveChangesAsync();

        // Reload with student and course details for response
        var updatedEnrollment = await _unitOfWork.Enrollments.GetWithDetailsAsync(id);

        // Return mapped response
        return _mapper.Map<EnrollmentResponseDto>(updatedEnrollment);
    }

    /// <summary>
    /// Delete an enrollment asynchronously
    /// </summary>
    public async Task<bool> DeleteEnrollmentAsync(int id)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
        if (enrollment == null)
        {
            return false;
        }

        _unitOfWork.Enrollments.Delete(enrollment);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Check if an enrollment exists asynchronously
    /// </summary>
    public async Task<bool> EnrollmentExistsAsync(int id)
    {
        return await _unitOfWork.Enrollments.ExistsAsync(id);
    }

    /// <summary>
    /// Check if a student is already enrolled in a course asynchronously
    /// </summary>
    public async Task<bool> IsStudentEnrolledAsync(int studentId, int courseId)
    {
        return await _unitOfWork.Enrollments.IsEnrolledAsync(studentId, courseId);
    }
}