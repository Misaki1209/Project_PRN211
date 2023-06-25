using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SubjectPages;

public class CreateSubject : PageModel
{
    private ISubjectRepository _subjectRepository;
    private IMapper _mapper;
    public SubjectDto Subject { get; set; }

    public CreateSubject(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(SubjectDto subject)
    {
        subject.SubjectId = _subjectRepository.GetNextId();
        subject.Deleted = Common.Status.Active;
        subject.MarkAvailable = Common.AvailableStatus.UnAvailable;
        _subjectRepository.AddSubject(subject);
        return RedirectToPage("Index");
    }
}