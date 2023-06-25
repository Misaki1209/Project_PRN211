
using Domain.Constants;

namespace Infrastructure.Dtos;

public class SemesterDto
{
    public int SemesterId { get; set; }

    public string SemesterName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Deleted { get; set; }
}