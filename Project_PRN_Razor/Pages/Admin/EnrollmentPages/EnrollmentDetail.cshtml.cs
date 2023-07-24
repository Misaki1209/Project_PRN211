using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.EnrollmentPages;

[Authorize(Roles = "Admin")]
public class EnrollmentDetail : PageModel
{
    private IEnrollmentRepository _enrollmentRepository;
    private ISubjectRepository _subjectRepository;
    private IMarkReportRepository _markReportRepository;
    private IMapper _mapper;
    
    public EnrollmentDto EnrollmentModel { get; set; }
    [BindProperty]
    public List<MarkReportDto> MarkList { get; set; }


    public EnrollmentDetail(IEnrollmentRepository enrollmentRepository, ISubjectRepository subjectRepository, IMarkReportRepository markReportRepository, IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _subjectRepository = subjectRepository;
        _markReportRepository = markReportRepository;
        _mapper = mapper;
    }   
    
    public void OnGet(int enrollmentId)
    {
        EnrollmentModel = _enrollmentRepository.GetEnrollmentDetail(enrollmentId);
        var markList = _markReportRepository.GetMarkReportByEnrollmentId(enrollmentId);
        MarkList = markList.Select(x => new MarkReportDto
        {
            MarkId = x.MarkId,
            MarkName = x.Mark.MarkName,
            MarkValue = x.MarkValue,
            Coefficient = x.Mark.Coefficient * 100 + "%"
        }).ToList();
    }

    public IActionResult OnPost(int enrollmentId)
    {
        foreach (var mark in MarkList)
        {
            _markReportRepository.UpdateMarkValue(enrollmentId, mark.MarkId, mark.MarkValue);
        }
        _enrollmentRepository.UpdateFinalMark(enrollmentId);
        return RedirectToPage("EnrollmentDetail",new{enrollmentId = enrollmentId});
    }
}