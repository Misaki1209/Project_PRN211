using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class ClassRepository: IClassRepository
{
    private ProjectPrn221Context _context;

    public ClassRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Class> GetAllClasses()
    {
        try
        {
            return _context.Classes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Class? GetClassById(int id)
    {
        try
        {
            return _context.Classes.FirstOrDefault(x => x.ClassId == id);
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
            return _context.Classes.OrderByDescending(x => x.ClassId).First().ClassId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddClass(Class addClass)
    {
        try
        {
            _context.Classes.Add(addClass);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateClass(Class updateClass)
    {
        try
        {
            var updateObj = _context.Classes.FirstOrDefault(x => x.ClassId == updateClass.ClassId);
            if (updateObj == null)
            {
                throw new Exception($"Class with id: {updateClass.ClassId} not found");
            }
            
            updateObj.ClassName = updateClass.ClassName;
            _context.Classes.Update(updateObj);
            _context.SaveChanges();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteClass(int id)
    {
        try
        {
            var updateObj = _context.Classes.FirstOrDefault(x => x.ClassId == id);
            if (updateObj == null)
            {
                throw new Exception($"Class with id: {id} not found");
            }

            if (updateObj.Deleted == true)
            {
                throw new Exception("Class with id: {id} has been deleted");
            }

            updateObj.Deleted = true;
            _context.Classes.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}