using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class Mark
{
    public int MarkId { get; set; }

    public string MarkName { get; set; } = null!;

    public double Coefficient { get; set; }
    public int? Resit { get; set; }

    public bool Deleted { get; set; }
    //public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    public virtual ICollection<SubjectMark> SubjectMarks { get; set; } = new List<SubjectMark>();
}
