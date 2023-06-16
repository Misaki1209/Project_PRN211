using Domain.Models;

namespace Domain.IRepositories;

public interface IMarkRepository
{
    public IEnumerable<Mark> GetAllMarks();
    public Mark? GetMarkById(int id);
    public int GetNextId();
    public void AddMark(Mark mark);
    public void UpdateMark(Mark mark);
    public void DeleteMark(int id);
}