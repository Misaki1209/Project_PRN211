using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class MarkRepository : IMarkRepository
{
    private ProjectPrn221Context _context;

    public MarkRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Mark> GetAllMarks()
    {
        try
        {
            return _context.Marks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Mark? GetMarkById(int id)
    {
        try
        {
            return _context.Marks.FirstOrDefault(x => x.MarkId == id);
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
            return _context.Marks.OrderByDescending(x => x.MarkId).First().MarkId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddMark(Mark mark)
    {
        try
        {
            _context.Marks.Add(mark);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMark(Mark mark)
    {
        var updateObj = _context.Marks.FirstOrDefault(x => x.MarkId == mark.MarkId);
        if (updateObj == null)
        {
            throw new Exception($"Mark id: {mark.MarkId} not found");
        }

        updateObj.Coefficient = mark.Coefficient;
        updateObj.MarkName = mark.MarkName;
        updateObj.Resit = mark.Resit;
        _context.Marks.Update(updateObj);
        _context.SaveChanges();
    }

    public void DeleteMark(int id)
    {
        var updateObj = _context.Marks.FirstOrDefault(x => x.MarkId == id);
        if (updateObj == null)
        {
            throw new Exception($"Mark id: {id} not found");
        }

        if (updateObj.Deleted)
        {
            throw new Exception($"Mark id: {id} has been deleted");
        }

        updateObj.Deleted = true;
        _context.Marks.Update(updateObj);
        _context.SaveChanges();
    }
}