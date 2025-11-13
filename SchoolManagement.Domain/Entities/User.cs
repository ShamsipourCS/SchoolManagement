using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a user in the school management system for authentication and authorization.
/// This entity handles identity concerns only. Domain-specific data lives in profile entities.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private User() { }

    /// <summary>
    /// Unique username for authentication
    /// </summary>
    public string Username { get; private set; } = string.Empty;

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>
    /// BCrypt hashed password
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// User's role in the system (Admin, Teacher, Student)
    /// </summary>
    public UserRole Role { get; private set; }

    /// <summary>
    /// Indicates whether the user account is active
    /// </summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>
    /// Navigation property to teacher profile (if user is a teacher)
    /// </summary>
    public virtual TeacherProfile? TeacherProfile { get; private set; }

    /// <summary>
    /// Navigation property to student profile (if user is a student)
    /// </summary>
    public virtual StudentProfile? StudentProfile { get; private set; }

    /// <summary>
    /// Factory method to create a new user with validation
    /// </summary>
    /// <param name="username">Unique username</param>
    /// <param name="email">Email address</param>
    /// <param name="passwordHash">BCrypt hashed password</param>
    /// <param name="role">User role</param>
    /// <returns>A valid User instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static User Create(string username, string email, string passwordHash, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required", nameof(username));

        if (username.Length < 3 || username.Length > 50)
            throw new ArgumentException("Username must be between 3 and 50 characters", nameof(username));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email format", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        return new User
        {
            Username = username.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            Role = role,
            IsActive = true
        };
    }

    /// <summary>
    /// Updates the user's email
    /// </summary>
    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email format", nameof(email));

        Email = email.Trim().ToLowerInvariant();
    }

    /// <summary>
    /// Updates the user's password hash
    /// </summary>
    public void UpdatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Deactivates the user account
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the user account
    /// </summary>
    public void Activate()
    {
        IsActive = true;
    }
}
