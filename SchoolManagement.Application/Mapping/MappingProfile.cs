using AutoMapper;
using SchoolManagement.Application.DTOs.Courses;
using SchoolManagement.Application.DTOs.Enrollments;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Application.DTOs.Teachers;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Mapping;

/// <summary>
/// AutoMapper profile for mapping between domain entities and DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateStudentMappings();
        CreateTeacherMappings();
        CreateCourseMappings();
        CreateEnrollmentMappings();
    }

    /// <summary>
    /// Configure mappings for Student entity
    /// </summary>
    private void CreateStudentMappings()
    {
        // Student -> StudentResponseDto
        CreateMap<Student, StudentResponseDto>()
            .ForMember(dest => dest.EnrollmentCount,
                opt => opt.MapFrom(src => src.Enrollments.Count));

        // StudentCreateDto -> Student
        // Note: We can't map directly to Student because it uses factory method
        // This mapping is for data transfer only - actual entity creation should use Student.Create()
        CreateMap<StudentCreateDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

        // StudentUpdateDto -> Student
        CreateMap<StudentUpdateDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore());
    }

    /// <summary>
    /// Configure mappings for Teacher entity
    /// </summary>
    private void CreateTeacherMappings()
    {
        // Teacher -> TeacherResponseDto
        CreateMap<Teacher, TeacherResponseDto>()
            .ForMember(dest => dest.CourseCount,
                opt => opt.MapFrom(src => src.Courses.Count));

        // TeacherCreateDto -> Teacher
        CreateMap<TeacherCreateDto, Teacher>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Courses, opt => opt.Ignore());

        // TeacherUpdateDto -> Teacher
        CreateMap<TeacherUpdateDto, Teacher>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Courses, opt => opt.Ignore());
    }

    /// <summary>
    /// Configure mappings for Course entity
    /// </summary>
    private void CreateCourseMappings()
    {
        // Course -> CourseResponseDto
        CreateMap<Course, CourseResponseDto>()
            .ForMember(dest => dest.TeacherName,
                opt => opt.MapFrom(src => src.Teacher.FullName))
            .ForMember(dest => dest.EnrollmentCount,
                opt => opt.MapFrom(src => src.Enrollments.Count));

        // CourseCreateDto -> Course
        CreateMap<CourseCreateDto, Course>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

        // CourseUpdateDto -> Course
        CreateMap<CourseUpdateDto, Course>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore());
    }

    /// <summary>
    /// Configure mappings for Enrollment entity
    /// </summary>
    private void CreateEnrollmentMappings()
    {
        // Enrollment -> EnrollmentResponseDto
        CreateMap<Enrollment, EnrollmentResponseDto>()
            .ForMember(dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student.FullName))
            .ForMember(dest => dest.CourseTitle,
                opt => opt.MapFrom(src => src.Course.Title));

        // EnrollmentCreateDto -> Enrollment
        CreateMap<EnrollmentCreateDto, Enrollment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            .ForMember(dest => dest.Course, opt => opt.Ignore());

        // EnrollmentUpdateDto -> Enrollment
        CreateMap<EnrollmentUpdateDto, Enrollment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EnrollDate, opt => opt.Ignore())
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            .ForMember(dest => dest.Course, opt => opt.Ignore())
            .ForMember(dest => dest.StudentId, opt => opt.Ignore())
            .ForMember(dest => dest.CourseId, opt => opt.Ignore());
    }
}
