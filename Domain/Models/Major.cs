using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Major
{
    public int MajorId { get; set; }

    public string MajorName { get; set; } = null!;

    public bool Deleted { get; set; }
}
