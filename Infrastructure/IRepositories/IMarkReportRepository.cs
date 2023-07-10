using Domain.Models;

namespace Infrastructure.IRepositories;

public interface IMarkReportRepository
{
    public List<MarkReport> GetMarkReportByEnrollmentId(int enrollmentId);
    public void AddMarkReport(MarkReport newMarkReport);
    public void UpdateMarkReport(MarkReport updateMarkReport);
    public void DeleteMarkReport(MarkReport deleteMarkReport);
    public void UpdateMarkValue(int enrollmentId, int markId, double markValue);
}