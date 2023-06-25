using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface IMarkRepository
{
    public IEnumerable<MarkDto> GetAllMarks();
    public MarkDto? GetMarkById(int id);
    public int GetNextId();
    public void AddMark(MarkDto mark);
    public void UpdateMark(MarkDto mark);
    public void DeleteMark(int id);
}