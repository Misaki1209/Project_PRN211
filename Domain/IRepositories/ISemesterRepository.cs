using Domain.Models;

namespace Domain.IRepositories;

public interface ISemesterRepository
{
    public IEnumerable<Semester> GetAllSemester();
    public Semester? GetSemesterById(int id);
    public int GetNextId();
    public void AddSemester(Semester semester);
    public void UpdateSemester(Semester semester);
    public void DeleteSemester(int id);
}