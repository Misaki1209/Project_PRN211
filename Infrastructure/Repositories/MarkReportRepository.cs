using Domain.Models;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MarkReportRepository : IMarkReportRepository
{
    private ProjectPrn221Context _context;

    public MarkReportRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public List<MarkReport> GetMarkReportByEnrollmentId(int enrollmentId)
    {
        try
        {
            var cc =  _context.MarkReports.Include(x => x.Mark).Where(x => x.EnrollmentId == enrollmentId)
                .OrderBy(x => x.Mark.Coefficient)
                .ThenBy(x => x.Mark.MarkName).ToList();
            return cc;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddMarkReport(MarkReport newMarkReport)
    {
        try
        {
            _context.MarkReports.Add(newMarkReport);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMarkReport(MarkReport updateMarkReport)
    {
        try
        {
            var markReport = _context.MarkReports.FirstOrDefault(x => x.EnrollmentId == updateMarkReport.EnrollmentId && x.MarkId == updateMarkReport.MarkId);
            if (markReport == null)
                throw new Exception("Not found markreport with enrollmentId: " + updateMarkReport.EnrollmentId + " and MarkId: " + updateMarkReport.MarkId);
            markReport.MarkValue = updateMarkReport.MarkValue;
            _context.MarkReports.Update(markReport);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteMarkReport(MarkReport deleteMarkReport)
    {
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMarkValue(int enrollmentId, int markId, double markValue)
    {
        try
        {
            var markReport = _context.MarkReports.FirstOrDefault(x => x.EnrollmentId == enrollmentId && x.MarkId == markId);
            if (markReport == null)
                throw new Exception("Not found");
            if (markReport.MarkValue == markValue)
                return;
            markReport.MarkValue = markValue;
            _context.MarkReports.Update(markReport);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}