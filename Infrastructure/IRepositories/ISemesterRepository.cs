using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface ISemesterRepository
{
    public IEnumerable<SemesterDto> GetAllSemester();
    public SemesterDto? GetSemesterById(int id);
    public int GetNextId();
    public void AddSemester(SemesterDto semester);
    public void UpdateSemester(SemesterDto semester);
    public void DeleteSemester(int id);
}