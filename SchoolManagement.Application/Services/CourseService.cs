using AutoMapper;
using SchoolManagement.Application.DTOs.Courses;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Application.Services;

/// <summary>
/// Service implementation for course business operations
/// </summary>
public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all courses asynchronously
    /// </summary>
    public async Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    /// <summary>
    /// Get course by ID asynchronously
    /// </summary>
    public async Task<CourseResponseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        return course == null ? null : _mapper.Map<CourseResponseDto>(course);
    }

    /// <summary>
    /// Get course with teacher and enrollment details asynchronously
    /// </summary>
    public async Task<CourseResponseDto?> GetCourseWithDetailsAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetWithDetailsAsync(id);
        return course == null ? null : _mapper.Map<CourseResponseDto>(course);
    }

    /// <summary>
    /// Get all courses taught by a specific teacher asynchronously
    /// </summary>
    public async Task<IEnumerable<CourseResponseDto>> GetCoursesByTeacherIdAsync(int teacherId)
    {
        var courses = await _unitOfWork.Courses.GetByTeacherIdAsync(teacherId);
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    /// <summary>
    /// Create a new course asynchronously
    /// </summary>
    public async Task<CourseResponseDto> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        // Validate that teacher exists
        var teacherExists = await _unitOfWork.TeacherProfiles.ExistsAsync(courseCreateDto.TeacherProfileId);
        if (!teacherExists)
        {
            throw new ArgumentException($"Teacher with ID {courseCreateDto.TeacherProfileId} does not exist.",
                nameof(courseCreateDto.TeacherProfileId));
        }

        // Use domain factory method to create entity with validation
        var course = Course.Create(
            courseCreateDto.Title,
            courseCreateDto.TeacherProfileId,
            courseCreateDto.StartDate,
            courseCreateDto.Description);

        // Add to repository
        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.SaveChangesAsync();

        // Reload with teacher details for response
        var createdCourse = await _unitOfWork.Courses.GetWithDetailsAsync(course.Id);

        // Return mapped response
        return _mapper.Map<CourseResponseDto>(createdCourse);
    }

    /// <summary>
    /// Update an existing course asynchronously
    /// </summary>
    public async Task<CourseResponseDto?> UpdateCourseAsync(int id, CourseUpdateDto courseUpdateDto)
    {
        // Check if course exists
        var existingCourse = await _unitOfWork.Courses.GetByIdAsync(id);
        if (existingCourse == null)
        {
            return null;
        }

        // Validate that new teacher exists (if teacher is being changed)
        if (courseUpdateDto.TeacherProfileId != existingCourse.TeacherProfileId)
        {
            var teacherExists = await _unitOfWork.TeacherProfiles.ExistsAsync(courseUpdateDto.TeacherProfileId);
            if (!teacherExists)
            {
                throw new ArgumentException($"Teacher with ID {courseUpdateDto.TeacherProfileId} does not exist.",
                    nameof(courseUpdateDto.TeacherProfileId));
            }
        }

        // Use domain methods to update entity
        existingCourse.UpdateTitle(courseUpdateDto.Title);
        existingCourse.UpdateDescription(courseUpdateDto.Description);
        existingCourse.UpdateStartDate(courseUpdateDto.StartDate);

        if (courseUpdateDto.TeacherProfileId != existingCourse.TeacherProfileId)
        {
            existingCourse.AssignTeacher(courseUpdateDto.TeacherProfileId);
        }

        // Update repository
        _unitOfWork.Courses.Update(existingCourse);
        await _unitOfWork.SaveChangesAsync();

        // Reload with teacher details for response
        var updatedCourse = await _unitOfWork.Courses.GetWithDetailsAsync(id);

        // Return mapped response
        return _mapper.Map<CourseResponseDto>(updatedCourse);
    }

    /// <summary>
    /// Delete a course asynchronously
    /// </summary>
    public async Task<bool> DeleteCourseAsync(int id)
    {
        // Get course with enrollments to check if there are active enrollments
        var course = await _unitOfWork.Courses.GetWithDetailsAsync(id);
        if (course == null)
        {
            return false;
        }

        // Prevent deletion if course has active enrollments
        if (course.Enrollments.Any())
        {
            throw new InvalidOperationException(
                $"Cannot delete course with ID {id} because it has {course.Enrollments.Count} active enrollment(s). Please remove enrollments first.");
        }

        _unitOfWork.Courses.Delete(course);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Check if a course exists asynchronously
    /// </summary>
    public async Task<bool> CourseExistsAsync(int id)
    {
        return await _unitOfWork.Courses.ExistsAsync(id);
    }
}