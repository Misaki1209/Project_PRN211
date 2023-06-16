using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class MajorRepository : IMajorRepository
{
    private ProjectPrn221Context _context;

    public MajorRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Major> GetAllMajors()
    {
        try
        {
            return _context.Majors;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Major? GetMajorById(int id)
    {
        try
        {
            return _context.Majors.FirstOrDefault(x => x.MajorId == id);
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
            return _context.Majors.OrderByDescending(x => x.MajorId).First().MajorId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddMajor(Major major)
    {
        try
        {
            _context.Majors.Add(major);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMajor(Major major)
    {
        try
        {
            var updateObj = _context.Majors.FirstOrDefault(x => x.MajorId == major.MajorId);
            if (updateObj == null)
            {
                throw new Exception($"Major id: {major.MajorId} not found");
            }

            updateObj.MajorName = major.MajorName;
            _context.Majors.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteMajor(int id)
    {
        try
        {
            var updateObj = _context.Majors.FirstOrDefault(x => x.MajorId == id);
            if (updateObj == null)
            {
                throw new Exception($"Major id: {id} not found");
            }

            if (updateObj.Deleted)
            {
                throw new Exception($"Major id: {id} has been deleted");
            }

            updateObj.Deleted = true;
            _context.Majors.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}