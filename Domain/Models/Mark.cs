using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Mark
{
    public int MarkId { get; set; }

    public string MarkName { get; set; } = null!;

    public double Coefficient { get; set; }

    public bool Deleted { get; set; }
}
