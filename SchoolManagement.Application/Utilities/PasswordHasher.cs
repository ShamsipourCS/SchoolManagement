namespace SchoolManagement.Application.Utilities;

/// <summary>
/// Utility class for hashing and verifying passwords using BCrypt
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Hashes a password using BCrypt with automatic salt generation
    /// </summary>
    /// <param name="password">Plain text password to hash</param>
    /// <returns>BCrypt hashed password</returns>
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verifies a password against a BCrypt hash
    /// </summary>
    /// <param name="password">Plain text password to verify</param>
    /// <param name="passwordHash">BCrypt hash to verify against</param>
    /// <returns>True if password matches hash, false otherwise</returns>
    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
