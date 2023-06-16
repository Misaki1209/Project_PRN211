using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class MarkReport
{
    public int EnrollmentId { get; set; }

    public int MarkId { get; set; }

    public double MarkValue { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;

    public virtual Mark Mark { get; set; } = null!;
}
