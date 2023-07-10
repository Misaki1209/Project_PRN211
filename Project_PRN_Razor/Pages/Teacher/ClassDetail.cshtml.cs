using System.Security.Claims;
using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Teacher;
[Authorize(Roles = "Teacher")]
public class ClassDetail : PageModel
{
    private ISemesterRepository _semesterRepository;
    private ISubjectRepository _subjectRepository;
    private IClassRepository _classRepository;
    private IEnrollmentRepository _enrollmentRepository;

    public SemesterDto? SelectedSemester { get; set; }
    public SubjectDto? SelectedSubject { get; set; }
    public ClassDto? SelectedClass { get; set; }
    
    private IMapper _mapper;

    public List<EnrollmentForClassList> EnrollmentList { get; set; }
    public ClassDetail(ISemesterRepository semesterRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, IEnrollmentRepository enrollmentRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _subjectRepository = subjectRepository;
        _classRepository = classRepository;
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }
    
    public void OnGet(int selectedSemesterId, int selectedSubjectId, int selectedClassId)
    {
        SelectedClass = _classRepository.GetClassById(selectedClassId);
        SelectedSemester = _semesterRepository.GetSemesterById(selectedSemesterId);
        SelectedSubject = _subjectRepository.GetSubjectById(selectedSubjectId);
        EnrollmentList = _enrollmentRepository.GetClassDetail(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId, selectedSubjectId, selectedClassId);
    }
}