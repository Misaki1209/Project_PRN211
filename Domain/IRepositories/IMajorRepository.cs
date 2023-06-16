using Domain.Models;

namespace Domain.IRepositories;

public interface IMajorRepository
{
    public IEnumerable<Major> GetAllMajors();
    public Major? GetMajorById(int id);
    public int GetNextId();
    public void AddMajor(Major major);
    public void UpdateMajor(Major major);
    public void DeleteMajor(int id);
}