using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface IEnrollmentRepository
{
    public IEnumerable<Enrollment> GetAllEnrollments();
    public IEnumerable<Enrollment> GetEnrollmentByTeacherId(int teacherId);
    public List<int> GetSubjectIdListByTeacher(int teacherId, int semesterId);
    public List<int> GetSubjectIdListByStudent(int studentId, int semesterId);
    public List<int> GetSubjctIdListByAdmin(int semesterId);
    public List<int> GetClassIdListByTeacher(int teacherId, int semesterId, int subjectId);
    public List<int> GetClassIdListByAdmin(int semesterId, int subjectId);
    public List<EnrollmentForClassList> GetClassDetail(int teacherId, int semesterId, int subjectId, int classId);
    public List<EnrollmentForClassList> GetClassDetailByAdmin(int semesterId, int subjectId, int classId);
    public EnrollmentDto GetEnrollmentDetail(int enrollmentId);
    public EnrollmentDto? GetEnrollmentDetailByStudent(int studentId, int semesterId, int subjectId);
    public void UpdateFinalMark(int enrollmentId);
    public void AddEnrollment(Enrollment enrollment);
    public int GetNextId();
}