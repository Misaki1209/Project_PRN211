using Domain.Models;

namespace Infrastructure.Dtos;

public class EnrollmentDto
{
    public int EnrollmentId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public double? FinalMark { get; set; }

    public int SemesterId { get; set; }

    public bool Status { get; set; }

    public Class Class { get; set; } = null!;

    public Semester Semester { get; set; } = null!;

    public Account Student { get; set; } = null!;

    public Subject Subject { get; set; } = null!;

    public Account Teacher { get; set; } = null!;
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
}

public class EnrollmentForClassList
{
    public int EnrollmentId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public int SemesterId { get; set; }
    
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
}