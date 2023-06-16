using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class StudentDetail
{
    public int AccountId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public int MajorId { get; set; }

    public int StartYear { get; set; }

    public int EndYear { get; set; }

    public bool Status { get; set; }

    public bool Deleted { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Major Major { get; set; } = null!;
}
