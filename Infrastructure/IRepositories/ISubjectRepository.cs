using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface ISubjectRepository
{
    public IEnumerable<SubjectDto> GetAllSubjects();
    public SubjectDto? GetSubjectById(int id);
    public int GetNextId();
    public void AddSubject(SubjectDto subject);
    public void UpdateSubject(SubjectDto subject);
    public void DeleteSubject(int id);
    public SubjectDetailDto? GetSubjectDetailById(int id);
    public void AddMarkToSubject(int subjectId, int markId);
    public void DeleteMarkToSubject(int subjectId, int markId);
}