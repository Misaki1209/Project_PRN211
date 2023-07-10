using System.Security.Claims;
using AutoMapper;
using Domain.Constants;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Student;

public class Index : PageModel
{
    private ISemesterRepository _semesterRepository;
    private ISubjectRepository _subjectRepository;
    private IClassRepository _classRepository;
    private IEnrollmentRepository _enrollmentRepository;
    private IMarkReportRepository _markReportRepository;
    private IMapper _mapper;
    
    public IEnumerable<SemesterDto> Semesters { get; set; }
    public IEnumerable<SubjectDto> Subjects { get; set; }
    public IEnumerable<ClassDto> Classes { get; set; }
    
    public SemesterDto? SelectedSemester { get; set; }
    public SubjectDto? SelectedSubject { get; set; }
    
    public int? SelectedSemesterId { get; set; }
    public int? SelectedSubjectId { get; set; }
    
    public string? StudentAccountId { get; set; }
    public EnrollmentDto EnrollmentModel { get; set; }
    public List<MarkReportDto> MarkList { get; set; }
    
    
    public Index(IMapper mapper, ISemesterRepository semesterRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, IEnrollmentRepository enrollmentRepository, IMarkReportRepository markReportRepository)
    {
        _semesterRepository = semesterRepository;
        _subjectRepository = subjectRepository;
        _classRepository = classRepository;
        _enrollmentRepository = enrollmentRepository;
        _markReportRepository = markReportRepository;
        _mapper = mapper;
    }   
    public void OnGet()
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
    }

    public IActionResult OnPostLoadSubject(int selectedSemesterId)
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        SelectedSemesterId = selectedSemesterId;
        var subjectIdList = _enrollmentRepository.GetSubjectIdListByStudent(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId);
        Subjects = _subjectRepository.GetAllSubjects().Where(x => subjectIdList.Contains(x.SubjectId));
        return Page();
    }

    public IActionResult OnPostLoadMarkReport(int selectedSemesterId, int selectedSubjectId)
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        SelectedSemesterId = selectedSemesterId;
        var subjectIdList = _enrollmentRepository.GetSubjectIdListByStudent(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId);
        Subjects = _subjectRepository.GetAllSubjects().Where(x => subjectIdList.Contains(x.SubjectId));
        EnrollmentModel = _enrollmentRepository.GetEnrollmentDetailByStudent(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId, selectedSubjectId);
        var markList = _markReportRepository.GetMarkReportByEnrollmentId(EnrollmentModel.EnrollmentId);
        MarkList = markList.Select(x => new MarkReportDto
        {
            MarkId = x.MarkId,
            MarkName = x.Mark.MarkName,
            MarkValue = x.MarkValue,
            Coefficient = x.Mark.Coefficient * 100 + "%"
        }).ToList();
        return Page();
    }
}