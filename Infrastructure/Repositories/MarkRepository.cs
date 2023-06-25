using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Repositories;

public class MarkRepository : IMarkRepository
{
    private ProjectPrn221Context _context;
    private IMapper _mapper;

    public MarkRepository(ProjectPrn221Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<MarkDto> GetAllMarks()
    {
        try
        {
            return _mapper.Map<IEnumerable<MarkDto>>(_context.Marks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public MarkDto? GetMarkById(int id)
    {
        try
        {
            var markReadModel =  _context.Marks.FirstOrDefault(x => x.MarkId == id);
            return _mapper.Map<MarkDto>(markReadModel);
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

    public void AddMark(MarkDto markDto)
    {
        try
        {
            var mark = _mapper.Map<Mark>(markDto);
            _context.Marks.Add(mark);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateMark(MarkDto markDto)
    {
        var mark = _mapper.Map<Mark>(markDto);
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