using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface IMajorRepository
{
    public IEnumerable<MajorDto> GetAllMajors();
    public MajorDto? GetMajorById(int id);
    public int GetNextId();
    public void AddMajor(MajorDto major);
    public void UpdateMajor(MajorDto major);
    public void DeleteMajor(int id);
}