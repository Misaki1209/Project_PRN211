using Domain.Models;

namespace Domain.IRepositories;

public interface ISubjectRepository
{
    public IEnumerable<Subject> GetAllSubjects();
    public Subject? GetSubjectById(int id);
    public int GetNextId();
    public void AddSubject(Subject subject);
    public void UpdateSubject(Subject subject);
    public void DeleteSubject(int id);
}