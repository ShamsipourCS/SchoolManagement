using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student's enrollment in a course
/// </summary>
public class Enrollment : BaseEntity
{
    /// <summary>
    /// Date when the student enrolled in the course
    /// </summary>
    public DateTime EnrollDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    public decimal? Grade { get; set; }

    /// <summary>
    /// Foreign key to the student
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Navigation property to the student
    /// </summary>
    public Student Student { get; set; } = null!;

    /// <summary>
    /// Foreign key to the course
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Navigation property to the course
    /// </summary>
    public Course Course { get; set; } = null!;
}