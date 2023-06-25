using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Repositories;

public class MajorRepository : IMajorRepository
{
    private ProjectPrn221Context _context;
    private IMapper _mapper;

    public MajorRepository(ProjectPrn221Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<MajorDto> GetAllMajors()
    {
        try
        {
            return _mapper.Map<IEnumerable<MajorDto>>(_context.Majors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public MajorDto? GetMajorById(int id)
    {
        try
        {
            var majorReadModel =  _context.Majors.FirstOrDefault(x => x.MajorId == id);
            return _mapper.Map<MajorDto>(majorReadModel);
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

    public void AddMajor(MajorDto major)
    {
        try
        {
            var majorReadModel = _mapper.Map<Major>(major);
            _context.Majors.Add(majorReadModel);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMajor(MajorDto majorDto)
    {
        try
        {
            var major = _mapper.Map<Major>(majorDto);
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