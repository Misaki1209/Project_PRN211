using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private ProjectPrn221Context _context;
    public SubjectRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Subject> GetAllSubjects()
    {
        try
        {
            return _context.Subjects;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Subject? GetSubjectById(int id)
    {
        try
        {
            return _context.Subjects.FirstOrDefault(x => x.SubjectId == id);
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
            return _context.Subjects.OrderByDescending(x => x.SubjectId).First().SubjectId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddSubject(Subject subject)
    {
        try
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateSubject(Subject subject)
    {
        try
        {
            var updateObj = _context.Subjects.FirstOrDefault(x => x.SubjectId == subject.SubjectId);
            if (updateObj == null)
            {
                throw new Exception($"Subject id: {subject.SubjectId} not found");
            }

            updateObj.SubjectCode = subject.SubjectCode;
            updateObj.SubjectName = subject.SubjectName;
            _context.Subjects.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteSubject(int id)
    {
        try
        {
            var updateObj = _context.Subjects.FirstOrDefault(x => x.SubjectId == id);
            if (updateObj == null)
            {
                throw new Exception($"Subject id: {id} not found");
            }

            if (updateObj.Deleted)
            {
                throw new Exception($"Subject id: {id} has been deleted");
            }

            updateObj.Deleted = true;
            _context.Subjects.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}