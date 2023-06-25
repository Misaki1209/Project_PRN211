using AutoMapper;
using Domain.Constants;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SubjectPages;

public class DetailSubject : PageModel
{
    private ISubjectRepository _subjectRepository;
    private IMarkRepository _markRepository;
    private ISubjectMarkRepository _subjectMarkRepository;
    private IMapper _mapper;
    
    public SubjectDetailDto? Subject { get; set; }
    public IEnumerable<MarkDto> MarkList { get; set; }
    public int SelectedMarkId { get; set; }
    public int SelectedSubjectId { get; set; }

    public DetailSubject(ISubjectRepository subjectRepository, IMarkRepository markRepository, ISubjectMarkRepository subjectMarkRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _markRepository = markRepository;
        _subjectMarkRepository = subjectMarkRepository;
        _mapper = mapper;
    }
    
    public void OnGet(int subjectId)
    {
        Subject = _subjectRepository.GetSubjectDetailById(subjectId);
        var addedMarkList = Subject.MarkList.Select(subjectMark => subjectMark.MarkId).ToList();
        MarkList = _markRepository.GetAllMarks().Where(x => x.Deleted.Equals(Common.Status.Active) && !addedMarkList.Contains(x.MarkId));
    }

    public IActionResult OnPostAddMark(int selectedSubjectId, int selectedMarkId)
    {
        if (selectedSubjectId != 0 && selectedMarkId != 0)
        {
            _subjectRepository.AddMarkToSubject(selectedSubjectId, selectedMarkId);   
        }
        return RedirectToPage("DetailSubject", new {id = selectedSubjectId});
    }

    public IActionResult OnPostDeleteMark(int selectedSubjectId, int selectedMarkId)
    {
        if (selectedSubjectId != 0 && selectedMarkId != 0)
        {
            _subjectRepository.DeleteMarkToSubject(selectedSubjectId, selectedMarkId);
        }

        return RedirectToPage("DetailSubject", new { id = selectedSubjectId});
    }
}