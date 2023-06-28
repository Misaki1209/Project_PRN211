using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private ProjectPrn221Context _context;
    private IMapper _mapper;
    public SubjectRepository(ProjectPrn221Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<SubjectDto> GetAllSubjects()
    {
        try
        {
            return _mapper.Map<IEnumerable<SubjectDto>>(_context.Subjects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public SubjectDto? GetSubjectById(int id)
    {
        try
        {
            var subjectReadModel =  _context.Subjects.FirstOrDefault(x => x.SubjectId == id);
            return _mapper.Map<SubjectDto>(subjectReadModel);
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

    public void AddSubject(SubjectDto subjectDto)
    {
        try
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateSubject(SubjectDto subjectDto)
    {
        try
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            var updateObj = _context.Subjects.FirstOrDefault(x => x.SubjectId == subject.SubjectId);
            if (updateObj == null)
            {
                throw new Exception($"Subject id: {subject.SubjectId} not found");
            }

            updateObj.SubjectCode = subject.SubjectCode;
            updateObj.SubjectName = subject.SubjectName;
            updateObj.MarkAvailable = subject.MarkAvailable;
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

    public SubjectDetailDto? GetSubjectDetailById(int id)
    {
        try
        {
            var subject = _context.Subjects.Include(x => x.SubjectMarks).ThenInclude(x => x.Mark).FirstOrDefault(x => x.SubjectId == id);
            return _mapper.Map<SubjectDetailDto>(subject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddMarkToSubject(int subjectId, int markId)
    {
        try
        {
            var subjectMark = new SubjectMark()
            {
                SubjectId = subjectId,
                MarkId = markId
            };
            var markReadModel = _context.Marks.FirstOrDefault(x => x.MarkId == markId);
            var subjectReadModel = _context.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            if (markReadModel.Resit != null)
            {
                var markResit = _context.Marks.FirstOrDefault(x => x.MarkId == markReadModel.Resit);
                if (markResit is { Deleted: false }
                    && subjectReadModel is { Deleted: false }
                    && !_context.SubjectMarks.Any(x => x.MarkId == markReadModel.Resit && x.SubjectId == subjectId))
                {
                    _context.SubjectMarks.Add(new SubjectMark()
                    {
                        SubjectId = subjectId,
                        MarkId = markResit.MarkId
                    });
                    _context.SaveChanges();
                }
            }
            if (markReadModel is { Deleted: false } 
                && subjectReadModel is { Deleted: false } 
                && !_context.SubjectMarks.Any(x => x.MarkId == markId && x.SubjectId == subjectId))
            {
                _context.SubjectMarks.Add(subjectMark);
                _context.SaveChanges();
                UpdateMarkAvailable(subjectId);
            }
            else
            {
                throw new Exception("Failed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteMarkToSubject(int subjectId, int markId)
    {
        try
        {
            var subjectMark = _context.SubjectMarks.FirstOrDefault(x => x.SubjectId == subjectId && x.MarkId == markId);
            if (subjectMark != null)
            {
                _context.SubjectMarks.Remove(subjectMark);
                _context.SaveChanges();
                UpdateMarkAvailable(subjectId);
            }
            else
            {
                throw new Exception("Not found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ApplySubject(int subjectId)
    {
        try
        {
            var subjectApply = _context.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            if (subjectApply == null)
                throw new Exception("Not found");
            if (subjectApply.Applied)
                throw new Exception("Already applied");
            subjectApply.Applied = true;
            _context.Subjects.Update(subjectApply);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void UpdateMarkAvailable(int subjectId)
    {
        try
        {
            var subject = _context.Subjects.Include(x => x.SubjectMarks).ThenInclude(x => x.Mark).FirstOrDefault(x => x.SubjectId == subjectId);
            var resitIdList = subject.SubjectMarks.Where(x => x.Mark.Resit != null).Select(x => x.Mark.Resit).ToList();
            var sumOfCoefficient = subject.SubjectMarks.Where(x => !resitIdList.Contains(x.MarkId)).Sum(x => x.Mark.Coefficient);
            if (sumOfCoefficient != 1) subject.MarkAvailable = false;
            else subject.MarkAvailable = true;
            _context.Subjects.Update(subject);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}