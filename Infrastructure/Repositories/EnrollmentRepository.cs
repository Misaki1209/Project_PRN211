using Domain.Models;
using Infrastructure.IRepositories;

namespace Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    public ProjectPrn221Context _context;

    public EnrollmentRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Enrollment> GetAllEnrollments()
    {
        try
        {
            return _context.Enrollments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Enrollment> GetEnrollmentByTeacherId(int teacherId)
    {
        throw new NotImplementedException();
    }

    public List<int> GetSubjectIdList(int teacherId, int semesterId)
    {
        throw new NotImplementedException();
    }

    public List<int> GetClassIdList(int teacherId, int semesterId, int subjectId)
    {
        throw new NotImplementedException();
    }
}