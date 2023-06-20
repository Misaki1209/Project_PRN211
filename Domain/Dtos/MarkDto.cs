namespace Domain.Dtos;

public class MarkDto
{
    public int MarkId { get; set; }

    public string MarkName { get; set; } = null!;

    public double Coefficient { get; set; }
    public int? Resit { get; set; }

    public string Deleted { get; set; }
}