using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    public bool Deleted { get; set; }
    public bool MarkAvailable { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    //public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
    public virtual ICollection<SubjectMark> SubjectMarks { get; set; } = new List<SubjectMark>();
}
