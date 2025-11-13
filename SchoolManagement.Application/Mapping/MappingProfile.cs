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
    /// Configure mappings for StudentProfile entity
    /// </summary>
    private void CreateStudentMappings()
    {
        // StudentProfile + User -> StudentResponseDto
        CreateMap<StudentProfile, StudentResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
            .ForMember(dest => dest.EnrollmentCount, opt => opt.MapFrom(src => src.Enrollments.Count));

        // Note: CreateDto and UpdateDto mappings are handled in services
        // because they require coordinated User and Profile creation/updates
    }

    /// <summary>
    /// Configure mappings for TeacherProfile entity
    /// </summary>
    private void CreateTeacherMappings()
    {
        // TeacherProfile + User -> TeacherResponseDto
        CreateMap<TeacherProfile, TeacherResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
            .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses.Count));

        // Note: CreateDto and UpdateDto mappings are handled in services
        // because they require coordinated User and Profile creation/updates
    }

    /// <summary>
    /// Configure mappings for Course entity
    /// </summary>
    private void CreateCourseMappings()
    {
        // Course -> CourseResponseDto
        CreateMap<Course, CourseResponseDto>()
            .ForMember(dest => dest.TeacherProfileId, opt => opt.MapFrom(src => src.TeacherProfileId))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.TeacherProfile.FullName))
            .ForMember(dest => dest.EnrollmentCount, opt => opt.MapFrom(src => src.Enrollments.Count));

        // CourseCreateDto -> Course (handled via Course.Create factory method)
        // CourseUpdateDto mappings handled in service layer
    }

    /// <summary>
    /// Configure mappings for Enrollment entity
    /// </summary>
    private void CreateEnrollmentMappings()
    {
        // Enrollment -> EnrollmentResponseDto
        CreateMap<Enrollment, EnrollmentResponseDto>()
            .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade != null ? src.Grade.Value : (decimal?)null))
            .ForMember(dest => dest.StudentProfileId, opt => opt.MapFrom(src => src.StudentProfileId))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.StudentProfile.FullName))
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
            .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title));

        // EnrollmentCreateDto -> Enrollment (handled via Enrollment.Create factory method)
    }
}
