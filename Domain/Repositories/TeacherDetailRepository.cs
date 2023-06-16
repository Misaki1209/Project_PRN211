using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class TeacherDetailRepository : ITeacherDetailRepository
{
    private ProjectPrn221Context _context;

    public TeacherDetailRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<TeacherDetail> GetAllTeacherDetails()
    {
        try
        {
            return _context.TeacherDetails;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public TeacherDetail? GetTeacherDetailById(int id)
    {
        try
        {
            return _context.TeacherDetails.FirstOrDefault(x => x.AccountId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddTeacherDetail(TeacherDetail teacherDetail)
    {
        try
        {
            _context.TeacherDetails.Add(teacherDetail);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateTeacherDetail(TeacherDetail teacherDetail)
    {
        try
        {
            var updateObj = _context.TeacherDetails.FirstOrDefault(x => x.AccountId == teacherDetail.AccountId);
            if (updateObj == null)
            {
                throw new Exception($"TeacherDetail id: {teacherDetail.AccountId} not found");
            }

            updateObj.Address = teacherDetail.Address;
            updateObj.Dob = teacherDetail.Dob;
            updateObj.FullName = teacherDetail.FullName;
            _context.TeacherDetails.Update(updateObj);
            _context.SaveChanges();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteTeacherDetail(int id)
    {
        var updateObj = _context.TeacherDetails.FirstOrDefault(x => x.AccountId == id);
        if (updateObj == null)
        {
            throw new Exception($"TeacherDetail id: {id} not found");
        }

        if (updateObj.Deleted)
        {
            throw new Exception($"TeacherDetail id: {id} has been deleted");
        }

        updateObj.Deleted = true;
        _context.TeacherDetails.Update(updateObj);
        _context.SaveChanges();
    }
}