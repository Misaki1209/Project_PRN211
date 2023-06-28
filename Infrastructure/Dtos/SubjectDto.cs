using Domain.Models;

namespace Infrastructure.Dtos;

public class SubjectDto
{
    public int SubjectId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    public string Deleted { get; set; }
    public string MarkAvailable { get; set; }
    public bool Applied { get; set; }
    
}

public class SubjectDetailDto
{
    public int SubjectId { get; set; }
    public string SubjectCode { get; set; }
    public string SubjectName { get; set; }
    public string Deleted { get; set; }
    public string MarkAvailable { get; set; }
    public bool Applied { get; set; }
    public List<SubjectMark> MarkList { get; set; }
}