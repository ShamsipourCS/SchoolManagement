using AutoMapper;
using SchoolManagement.Application.DTOs.Teachers;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Application.Services;

/// <summary>
/// Service implementation for teacher business operations
/// </summary>
public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all teachers asynchronously
    /// </summary>
    public async Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync()
    {
        var teachers = await _unitOfWork.TeacherProfiles.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
    }

    /// <summary>
    /// Get teacher by ID asynchronously
    /// </summary>
    public async Task<TeacherResponseDto?> GetTeacherByIdAsync(int id)
    {
        var teacher = await _unitOfWork.TeacherProfiles.GetByIdAsync(id);
        return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
    }

    /// <summary>
    /// Get teacher with course details asynchronously
    /// </summary>
    public async Task<TeacherResponseDto?> GetTeacherWithCoursesAsync(int id)
    {
        var teacher = await _unitOfWork.TeacherProfiles.GetWithCoursesAsync(id);
        return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
    }

    /// <summary>
    /// Get teacher by email address asynchronously
    /// </summary>
    public async Task<TeacherResponseDto?> GetTeacherByEmailAsync(string email)
    {
        var teacher = await _unitOfWork.TeacherProfiles.GetByEmailAsync(email);
        return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
    }

    /// <summary>
    /// Create a new teacher asynchronously
    /// </summary>
    public async Task<TeacherResponseDto> CreateTeacherAsync(TeacherCreateDto teacherCreateDto)
    {
        // Validate email uniqueness
        var emailExists = await _unitOfWork.TeacherProfiles.EmailExistsAsync(teacherCreateDto.Email);
        if (emailExists)
        {
            throw new ArgumentException($"A teacher with email '{teacherCreateDto.Email}' already exists.", nameof(teacherCreateDto.Email));
        }

        // Use domain factory method to create entity with validation
        var teacherProfile = TeacherProfileProfile.Create(teacherCreateDto.FullName, teacherCreateDto.Email, teacherCreateDto.HireDate);

        // Add to repository
        await _unitOfWork.TeacherProfiles.AddAsync(teacher);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<TeacherResponseDto>(teacher);
    }

    /// <summary>
    /// Update an existing teacher asynchronously
    /// </summary>
    public async Task<TeacherResponseDto?> UpdateTeacherAsync(int id, TeacherUpdateDto teacherUpdateDto)
    {
        // Check if teacher exists
        var existingTeacher = await _unitOfWork.TeacherProfiles.GetByIdAsync(id);
        if (existingTeacher == null)
        {
            return null;
        }

        // Validate email uniqueness (excluding current teacher)
        var emailExists = await _unitOfWork.TeacherProfiles.EmailExistsAsync(teacherUpdateDto.Email, id);
        if (emailExists)
        {
            throw new ArgumentException($"A teacher with email '{teacherUpdateDto.Email}' already exists.", nameof(teacherUpdateDto.Email));
        }

        // Use domain methods to update entity
        existingTeacher.UpdateFullName(teacherUpdateDto.FullName);
        existingTeacher.UpdateEmail(teacherUpdateDto.Email);

        // Note: HireDate cannot be updated as there's no domain method for it
        // This is intentional - hire dates should not change after creation

        // Update repository
        _unitOfWork.TeacherProfiles.Update(existingTeacher);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<TeacherResponseDto>(existingTeacher);
    }

    /// <summary>
    /// Delete a teacher asynchronously
    /// </summary>
    public async Task<bool> DeleteTeacherAsync(int id)
    {
        // Get teacher with courses to check if they have any assigned
        var teacher = await _unitOfWork.TeacherProfiles.GetWithCoursesAsync(id);
        if (teacher == null)
        {
            return false;
        }

        // Prevent deletion if teacher has assigned courses
        if (teacher.Courses.Any())
        {
            throw new InvalidOperationException($"Cannot delete teacher with ID {id} because they have {teacher.Courses.Count} assigned course(s). Please reassign or remove courses first.");
        }

        _unitOfWork.TeacherProfiles.Delete(teacher);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Check if a teacher exists asynchronously
    /// </summary>
    public async Task<bool> TeacherExistsAsync(int id)
    {
        return await _unitOfWork.TeacherProfiles.ExistsAsync(id);
    }

    /// <summary>
    /// Check if email is already in use by another teacher asynchronously
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null)
    {
        return await _unitOfWork.TeacherProfiles.EmailExistsAsync(email, excludeTeacherId);
    }
}
