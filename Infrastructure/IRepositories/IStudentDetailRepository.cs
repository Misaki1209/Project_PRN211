using Domain.Models;

namespace Infrastructure.IRepositories;

public interface IStudentDetailRepository
{
    public IEnumerable<StudentDetail> GetAllStudentDetails();
    public StudentDetail? GetStudentDetailById(int id);
    public void AddStudentDetail(StudentDetail studentDetail);
    public void UpdateStudentDetail(StudentDetail studentDetail);
    public void DeleteStudentDetail(int id);
}