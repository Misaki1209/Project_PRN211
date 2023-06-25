using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Repositories;

public class SemesterRepository : ISemesterRepository
{
    private ProjectPrn221Context _context;
    private IMapper _mapper;

    public SemesterRepository(ProjectPrn221Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<SemesterDto> GetAllSemester()
    {
        try
        {
            return _mapper.Map<IEnumerable<SemesterDto>>(_context.Semesters);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public SemesterDto? GetSemesterById(int id)
    {
        try
        {
            var semesterReadModel =  _context.Semesters.FirstOrDefault(x => x.SemesterId == id);
            return _mapper.Map<SemesterDto>(semesterReadModel);
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

    public void AddSemester(SemesterDto semesterDto)
    {
        try
        {
            var semester = _mapper.Map<Semester>(semesterDto);
            _context.Semesters.Add(semester);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateSemester(SemesterDto semesterDto)
    {
        try
        {
            var semester = _mapper.Map<Semester>(semesterDto);
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