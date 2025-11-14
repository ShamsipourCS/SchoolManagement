namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student profile in the school management system.
/// Contains domain-specific data for students. Identity/auth data is in User entity.
/// IsActive status is managed through User.IsActive.
/// </summary>
public class StudentProfile : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private StudentProfile()
    {
    }

    /// <summary>
    /// Foreign key to the User entity (1:1 relationship)
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Navigation property to the associated user
    /// </summary>
    public virtual User User { get; private set; } = null!;

    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Collection of enrollments for this student
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    /// <summary>
    /// Factory method to create a new student profile with validation
    /// </summary>
    /// <param name="userId">ID of the associated user</param>
    /// <param name="fullName">Student's full name</param>
    /// <param name="birthDate">Student's date of birth</param>
    /// <returns>A valid StudentProfile instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static StudentProfile Create(int userId, string fullName, DateTime birthDate)
    {
        if (userId <= 0)
            throw new ArgumentException("User ID must be a valid positive number", nameof(userId));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        if (birthDate >= DateTime.UtcNow)
            throw new ArgumentException("Birth date must be in the past", nameof(birthDate));

        if (birthDate < DateTime.UtcNow.AddYears(-120))
            throw new ArgumentException("Birth date is not realistic", nameof(birthDate));

        return new StudentProfile
        {
            UserId = userId,
            FullName = fullName.Trim(),
            BirthDate = birthDate
        };
    }

    /// <summary>
    /// Updates the student's full name
    /// </summary>
    /// <param name="fullName">New full name</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        FullName = fullName.Trim();
    }
}