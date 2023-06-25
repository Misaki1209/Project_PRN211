using Domain.Models;

namespace Infrastructure.IRepositories;

public interface ITeacherDetailRepository
{
    public IEnumerable<TeacherDetail> GetAllTeacherDetails();
    public TeacherDetail? GetTeacherDetailById(int id);
    public void AddTeacherDetail(TeacherDetail teacherDetail);
    public void UpdateTeacherDetail(TeacherDetail teacherDetail);
    public void DeleteTeacherDetail(int id);
}