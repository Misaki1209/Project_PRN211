using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

public class StudentDetailRepository : IStudentDetailRepository
{
    private ProjectPrn221Context _context;

    public StudentDetailRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<StudentDetail> GetAllStudentDetails()
    {
        try
        {
            return _context.StudentDetails;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public StudentDetail? GetStudentDetailById(int id)
    {
        try
        {
            return _context.StudentDetails.FirstOrDefault(x => x.AccountId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddStudentDetail(StudentDetail studentDetail)
    {
        try
        {
            _context.StudentDetails.Add(studentDetail);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateStudentDetail(StudentDetail studentDetail)
    {
        try
        {
            var updateObj = _context.StudentDetails.FirstOrDefault(x => x.AccountId == studentDetail.AccountId);
            if (updateObj == null)
            {
                throw new Exception($"StudentDetail id: {studentDetail.AccountId} not found");
            }

            updateObj.Address = studentDetail.Address;
            updateObj.Dob = studentDetail.Dob;
            updateObj.Status = studentDetail.Status;
            updateObj.StartYear = studentDetail.StartYear;
            updateObj.EndYear = studentDetail.EndYear;
            updateObj.MajorId = studentDetail.MajorId;
            updateObj.StudentCode = studentDetail.StudentCode;
            updateObj.FullName = studentDetail.FullName;
            _context.StudentDetails.Update(updateObj);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteStudentDetail(int id)
    {
        var updateObj = _context.StudentDetails.FirstOrDefault(x => x.AccountId == id);
        if (updateObj == null)
        {
            throw new Exception($"Student id: {id} not found");
        }

        if (updateObj.Deleted)
        {
            throw new Exception($"StudentDetail id: {id} has been deleted");
        }

        updateObj.Deleted = true;
        _context.StudentDetails.Update(updateObj);
        _context.SaveChanges();
    }
}