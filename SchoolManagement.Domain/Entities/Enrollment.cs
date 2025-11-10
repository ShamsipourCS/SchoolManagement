using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

public class Enrollment : BaseEntity
{
    public DateTime EnrollDate { get; set; } = DateTime.UtcNow;
    public decimal? Grade { get; set; }

    public int StudentId { get; set; }
    public virtual Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}
