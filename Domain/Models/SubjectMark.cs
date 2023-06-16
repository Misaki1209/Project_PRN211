using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SubjectMark
{
    public int SubjectId { get; set; }

    public int MarkId { get; set; }

    public virtual Mark Mark { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
