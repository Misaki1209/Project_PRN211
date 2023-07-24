using System.Security.Claims;
using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Teacher;
[Authorize(Roles = "Teacher")]
public class Index : PageModel
{
    private ISemesterRepository _semesterRepository;
    private ISubjectRepository _subjectRepository;
    private IClassRepository _classRepository;
    private IEnrollmentRepository _enrollmentRepository;
    private IMapper _mapper;
    
    public IEnumerable<SemesterDto> Semesters { get; set; }
    public IEnumerable<SubjectDto> Subjects { get; set; }
    public IEnumerable<ClassDto> Classes { get; set; }
    
    public SemesterDto? SelectedSemester { get; set; }
    public SubjectDto? SelectedSubject { get; set; }
    
    public int? SelectedSemesterId { get; set; }
    public int? SelectedSubjectId { get; set; }
    
    public string? TeacherAccountId { get; set; }

    public Index(IMapper mapper, ISemesterRepository semesterRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, IEnrollmentRepository enrollmentRepository)
    {
        _semesterRepository = semesterRepository;
        _subjectRepository = subjectRepository;
        _classRepository = classRepository;
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }   
    public void OnGet()
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        TeacherAccountId = User.FindFirstValue(ClaimTypes.Sid);
    }

    public IActionResult OnPostLoadSubject(int selectedSemesterId)
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        TeacherAccountId = User.FindFirstValue(ClaimTypes.Sid);
        SelectedSemesterId = selectedSemesterId;
        var subjectIdList = _enrollmentRepository.GetSubjectIdListByTeacher(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId);
        Subjects = _subjectRepository.GetAllSubjects().Where(x => subjectIdList.Contains(x.SubjectId));
        //return RedirectToPage("Index");
        //return JsonResult(Subjects);
        return Page();
    }

    public IActionResult OnPostLoadClass(int selectedSemesterId, int selectedSubjectId)
    {
        Semesters = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        SelectedSemesterId = selectedSemesterId;
        TeacherAccountId = User.FindFirstValue(ClaimTypes.Sid);
        var subjectIdList = _enrollmentRepository.GetSubjectIdListByTeacher(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId);
        Subjects = _subjectRepository.GetAllSubjects().Where(x => subjectIdList.Contains(x.SubjectId));
        SelectedSubjectId = selectedSubjectId;
        var classIdList = _enrollmentRepository.GetClassIdListByTeacher(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId, selectedSubjectId);
        Classes = _classRepository.GetAllClasses().Where(x => classIdList.Contains(x.ClassId));
        return Page();
    }
    
    
}