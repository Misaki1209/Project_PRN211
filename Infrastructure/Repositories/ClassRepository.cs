using AutoMapper;
using Infrastructure.Dtos;
using Domain.Models;
using Infrastructure.IRepositories;

namespace Infrastructure.Repositories;

public class ClassRepository: IClassRepository
{
    private ProjectPrn221Context _context;
    private IMapper _mapper;

    public ClassRepository(ProjectPrn221Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<ClassDto> GetAllClasses()
    {
        try
        {
            return _mapper.Map<IEnumerable<ClassDto>>(_context.Classes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ClassDto? GetClassById(int id)
    {
        try
        {
            var classById =  _context.Classes.FirstOrDefault(x => x.ClassId == id);
            return _mapper.Map<ClassDto>(classById);
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

    public void AddClass(ClassDto addClass)
    {
        try
        {
            var classToAdd = _mapper.Map<Class>(addClass);
            _context.Classes.Add(classToAdd);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateClass(ClassDto updateClass)
    {
        try
        {
            var classToUpdate = _mapper.Map<Class>(updateClass);
            var updateObj = _context.Classes.FirstOrDefault(x => x.ClassId == classToUpdate.ClassId);
            if (updateObj == null)
            {
                throw new Exception($"Class with id: {classToUpdate.ClassId} not found");
            }
            
            updateObj.ClassName = classToUpdate.ClassName;
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