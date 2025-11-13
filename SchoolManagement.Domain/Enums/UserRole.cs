namespace SchoolManagement.Domain.Enums;

/// <summary>
/// Enumeration of user roles in the school management system
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Student role - can view their own courses and grades
    /// </summary>
    Student = 0,

    /// <summary>
    /// Teacher role - can manage courses and student enrollments
    /// </summary>
    Teacher = 1,

    /// <summary>
    /// Administrator role - has full system access
    /// </summary>
    Admin = 2
}
