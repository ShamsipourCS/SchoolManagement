using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a course in the school management system
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// Title of the course
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the course
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the course starts
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Foreign key to the teacher teaching this course
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Navigation property to the teacher
    /// </summary>
    public virtual Teacher Teacher { get; set; } = null!;

    /// <summary>
    /// Collection of enrollments for this course
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}