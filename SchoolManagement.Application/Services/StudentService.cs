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
        var students = await _unitOfWork.Students.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }

    /// <summary>
    /// Get student by ID asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        return student == null ? null : _mapper.Map<StudentResponseDto>(student);
    }

    /// <summary>
    /// Get student with enrollment details asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> GetStudentWithEnrollmentsAsync(int id)
    {
        var student = await _unitOfWork.Students.GetWithEnrollmentsAsync(id);
        return student == null ? null : _mapper.Map<StudentResponseDto>(student);
    }

    /// <summary>
    /// Get all active students asynchronously
    /// </summary>
    public async Task<IEnumerable<StudentResponseDto>> GetActiveStudentsAsync()
    {
        var students = await _unitOfWork.Students.GetActiveStudentsAsync();
        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }

    /// <summary>
    /// Create a new student asynchronously
    /// </summary>
    public async Task<StudentResponseDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
    {
        // Use domain factory method to create entity with validation
        var student = Student.Create(studentCreateDto.FullName, studentCreateDto.BirthDate);

        // Set active status from DTO
        if (!studentCreateDto.IsActive)
        {
            student.Deactivate();
        }

        // Add to repository
        await _unitOfWork.Students.AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<StudentResponseDto>(student);
    }

    /// <summary>
    /// Update an existing student asynchronously
    /// </summary>
    public async Task<StudentResponseDto?> UpdateStudentAsync(int id, StudentUpdateDto studentUpdateDto)
    {
        // Check if student exists
        var existingStudent = await _unitOfWork.Students.GetByIdAsync(id);
        if (existingStudent == null)
        {
            return null;
        }

        // Use domain methods to update entity
        existingStudent.UpdateFullName(studentUpdateDto.FullName);

        // Update active status
        if (studentUpdateDto.IsActive && !existingStudent.IsActive)
        {
            existingStudent.Activate();
        }
        else if (!studentUpdateDto.IsActive && existingStudent.IsActive)
        {
            existingStudent.Deactivate();
        }

        // Note: BirthDate cannot be updated as there's no domain method for it
        // This is intentional - birth dates should not change after creation

        // Update repository
        _unitOfWork.Students.Update(existingStudent);
        await _unitOfWork.SaveChangesAsync();

        // Return mapped response
        return _mapper.Map<StudentResponseDto>(existingStudent);
    }

    /// <summary>
    /// Delete a student asynchronously
    /// </summary>
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        if (student == null)
        {
            return false;
        }

        _unitOfWork.Students.Delete(student);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Check if a student exists asynchronously
    /// </summary>
    public async Task<bool> StudentExistsAsync(int id)
    {
        return await _unitOfWork.Students.ExistsAsync(id);
    }
}
