using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class SemesterRepository : ISemesterRepository
{
    private ProjectPrn221Context _context;

    public SemesterRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Semester> GetAllSemester()
    {
        try
        {
            return _context.Semesters;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Semester? GetSemesterById(int id)
    {
        try
        {
            return _context.Semesters.FirstOrDefault(x => x.SemesterId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public int GetNextId()
    {
        try
        {
            return _context.Semesters.OrderByDescending(x => x.SemesterId).First().SemesterId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddSemester(Semester semester)
    {
        try
        {
            _context.Semesters.Add(semester);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateSemester(Semester semester)
    {
        try
        {
            var updateObj = _context.Semesters.FirstOrDefault(x => x.SemesterId == semester.SemesterId);
            if (updateObj == null)
            {
                throw new Exception($"Semester id: {semester.SemesterId} not found");
            }

            updateObj.SemesterName = semester.SemesterName;
            updateObj.StartDate = semester.StartDate;
            updateObj.EndDate = semester.EndDate;
            _context.Semesters.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteSemester(int id)
    {
        try
        {
            var updateObj = _context.Semesters.FirstOrDefault(x => x.SemesterId == id);
            if (updateObj == null)
            {
                throw new Exception($"Semester id: {id} not found");
            }

            if (updateObj.Deleted)
            {
                throw new Exception($"Semester id: {id} has been deleted");
            }

            updateObj.Deleted = true;
            _context.Semesters.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}