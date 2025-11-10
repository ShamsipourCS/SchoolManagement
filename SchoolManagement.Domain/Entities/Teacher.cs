using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a teacher in the school management system
/// </summary>
public class Teacher : BaseEntity
{
    /// <summary>
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the teacher (must be unique)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Collection of courses taught by this teacher
    /// </summary>
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}