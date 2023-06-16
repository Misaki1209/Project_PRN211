using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TeacherDetail
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual Account Account { get; set; } = null!;
}
