using AutoMapper;
using Domain.Constants;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SubjectPages;
[Authorize(Roles = "Admin")]
public class EditSubject : PageModel
{
    private ISubjectRepository _subjectRepository;
    private IMapper _mapper;
    public SubjectDto? Subject { get; set; }
    public IEnumerable<SubjectDto> Subjects { get; set; }

    public EditSubject(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public void OnGet(int subjectId)
    {
        Subject = _subjectRepository.GetSubjectById(subjectId);
        Subjects = _subjectRepository.GetAllSubjects().Where(x => x.Deleted.Equals(Common.Status.Active));
    }
    public IActionResult OnPostUpdate(SubjectDto subject)
    {
        _subjectRepository.UpdateSubject(subject);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(SubjectDto subject)
    {
        _subjectRepository.DeleteSubject(subject.SubjectId);
        return RedirectToPage("Index");
    }
}