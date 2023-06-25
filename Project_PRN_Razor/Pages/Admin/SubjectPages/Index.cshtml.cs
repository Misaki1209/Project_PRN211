using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SubjectPages;

public class Index : PageModel
{
    private ISubjectRepository _subjectRepository;
    private IMapper _mapper;
    public IEnumerable<SubjectDto> Subjects;

    public Index(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        var subjectList = _subjectRepository.GetAllSubjects().Where(x => x.Deleted.Equals(Common.Status.Active));
        Subjects = _mapper.Map<IEnumerable<SubjectDto>>(subjectList);
    }
}