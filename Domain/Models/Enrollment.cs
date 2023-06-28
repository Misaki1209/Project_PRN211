using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public double? FinalMark { get; set; }

    public int SemesterId { get; set; }

    public bool Status { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Semester Semester { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Account Teacher { get; set; } = null!;
}
