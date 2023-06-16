using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Status { get; set; }

    public int Role { get; set; }

    public virtual ICollection<Enrollment> EnrollmentStudents { get; set; } = new List<Enrollment>();

    public virtual ICollection<Enrollment> EnrollmentTeachers { get; set; } = new List<Enrollment>();
}
