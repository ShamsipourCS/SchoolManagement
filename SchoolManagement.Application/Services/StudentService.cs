using AutoMapper;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Application.Services;

/// <summary>
/// Service implementation for student business operations
/// </summary>
public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all students asynchronously
    /// </summary>
    public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.StudentProfiles.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }

    /// <summary>
    /// Get student by ID asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        var student = await _unitOfWork.StudentProfiles.GetByIdAsync(id);
        return student == null ? null : _mapper.Map<StudentResponseDto>(student);
    }

    /// <summary>
    /// Get student with enrollment details asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> GetStudentWithEnrollmentsAsync(int id)
    {
        var student = await _unitOfWork.StudentProfiles.GetWithEnrollmentsAsync(id);
        return student == null ? null : _mapper.Map<StudentResponseDto>(student);
    }

    /// <summary>
    /// Get all active students asynchronously
    /// </summary>
    public async Task<IEnumerable<StudentResponseDto>> GetActiveStudentsAsync()
    {
        var students = await _unitOfWork.StudentProfiles.GetActiveStudentProfilesAsync();
        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }

    /// <summary>
    /// Create a new student asynchronously
    /// </summary>
    public async Task<StudentResponseDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
    {
        // Use domain factory method to create entity with validation
        var studentProfile = StudentProfile.Create(studentCreateDto.StudentId, studentCreateDto.FullName,
            studentCreateDto.BirthDate);

        // Add to repository
        await _unitOfWork.StudentProfiles.AddAsync(studentProfile);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<StudentResponseDto>(studentProfile);
    }

    /// <summary>
    /// Update an existing student asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> UpdateStudentAsync(int id, StudentUpdateDto studentUpdateDto)
    {
        // Check if student exists
        var existingStudent = await _unitOfWork.StudentProfiles.GetByIdAsync(id);
        if (existingStudent == null)
        {
            return null;
        }

        // Use domain methods to update entity
        existingStudent.UpdateFullName(studentUpdateDto.FullName);

        // Note: BirthDate cannot be updated as there's no domain method for it
        // This is intentional - birth dates should not change after creation

        // Update repository
        _unitOfWork.StudentProfiles.Update(existingStudent);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<StudentResponseDto>(existingStudent);
    }

    /// <summary>
    /// Delete a student asynchronously
    /// </summary>
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _unitOfWork.StudentProfiles.GetByIdAsync(id);
        if (student == null)
        {
            return false;
        }

        _unitOfWork.StudentProfiles.Delete(student);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Check if a student exists asynchronously
    /// </summary>
    public async Task<bool> StudentExistsAsync(int id)
    {
        return await _unitOfWork.StudentProfiles.ExistsAsync(id);
    }
}