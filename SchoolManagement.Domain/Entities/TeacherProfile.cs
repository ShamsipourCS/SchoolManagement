namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a teacher profile in the school management system.
/// Contains domain-specific data for teachers. Identity/auth data is in User entity.
/// </summary>
public class TeacherProfile : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private TeacherProfile()
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
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; private set; }

    /// <summary>
    /// Collection of courses taught by this teacher
    /// </summary>
    public virtual ICollection<Course> Courses { get; private set; } = new List<Course>();

    /// <summary>
    /// Factory method to create a new teacher profile with validation
    /// </summary>
    /// <param name="userId">ID of the associated user</param>
    /// <param name="fullName">Teacher's full name</param>
    /// <param name="hireDate">Date when teacher was hired</param>
    /// <returns>A valid TeacherProfile instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static TeacherProfile Create(int userId, string fullName, DateTime hireDate)
    {
        if (userId <= 0)
            throw new ArgumentException("User ID must be a valid positive number", nameof(userId));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required and cannot be empty", nameof(fullName));

        if (fullName.Length > 200)
            throw new ArgumentException("Full name cannot exceed 200 characters", nameof(fullName));

        if (hireDate > DateTime.UtcNow)
            throw new ArgumentException("Hire date cannot be in the future", nameof(hireDate));

        if (hireDate < DateTime.UtcNow.AddYears(-50))
            throw new ArgumentException("Hire date is not realistic", nameof(hireDate));

        return new TeacherProfile
        {
            UserId = userId,
            FullName = fullName.Trim(),
            HireDate = hireDate
        };
    }

    /// <summary>
    /// Updates the teacher's full name
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