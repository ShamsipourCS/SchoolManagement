using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student in the school management system
/// </summary>
public class Student : BaseEntity
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Collection of enrollments for this student
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}