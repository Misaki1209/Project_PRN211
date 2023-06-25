using Infrastructure.Dtos;
using Domain.Models;

namespace Infrastructure.IRepositories;

public interface IClassRepository
{
    public IEnumerable<ClassDto> GetAllClasses();
    public ClassDto? GetClassById(int id);
    public int GetNextId();
    public void AddClass(ClassDto addClass);
    public void UpdateClass(ClassDto updateClass);
    public void DeleteClass(int id);
}