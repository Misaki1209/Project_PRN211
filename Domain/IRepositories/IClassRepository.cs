using Domain.Models;

namespace Domain.IRepositories;

public interface IClassRepository
{
    public IEnumerable<Class> GetAllClasses();
    public Class? GetClassById(int id);
    public int GetNextId();
    public void AddClass(Class addClass);
    public void UpdateClass(Class updateClass);
    public void DeleteClass(int id);
}