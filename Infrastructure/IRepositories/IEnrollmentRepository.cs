using Domain.Models;

namespace Infrastructure.IRepositories;

public interface IEnrollmentRepository
{
    public IEnumerable<Enrollment> GetAllEnrollments();
    public IEnumerable<Enrollment> GetEnrollmentByTeacherId(int teacherId);
    public List<int> GetSubjectIdList(int teacherId, int semesterId);
    public List<int> GetClassIdList(int teacherId, int semesterId, int subjectId);
}