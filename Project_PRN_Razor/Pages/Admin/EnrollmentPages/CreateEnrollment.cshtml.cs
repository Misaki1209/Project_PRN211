using AutoMapper;
using Domain.Models;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.EnrollmentPages;
[Authorize(Roles = "Admin")]
public class CreateEnrollment : PageModel
{
    private ITeacherDetailRepository _teacherDetailRepository;
    private IStudentDetailRepository _studentDetailRepository;
    private ISemesterRepository _semesterRepository;
    private ISubjectRepository _subjectRepository;
    private IClassRepository _classRepository;
    private ISubjectMarkRepository _subjectMarkRepository;
    private IEnrollmentRepository _enrollmentRepository;
    private IMarkReportRepository _markReportRepository;
    private IMapper _mapper;

    public CreateEnrollment(ITeacherDetailRepository teacherDetailRepository, IStudentDetailRepository studentDetailRepository, ISemesterRepository semesterRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, ISubjectMarkRepository subjectMarkRepository, IEnrollmentRepository enrollmentRepository, IMarkReportRepository markReportRepository, IMapper mapper)
    {
        _teacherDetailRepository = teacherDetailRepository;
        _studentDetailRepository = studentDetailRepository;
        _semesterRepository = semesterRepository;
        _subjectRepository = subjectRepository;
        _classRepository = classRepository;
        _subjectMarkRepository = subjectMarkRepository;
        _enrollmentRepository = enrollmentRepository;
        _markReportRepository = markReportRepository;
        _mapper = mapper;
    }
    
    public List<TeacherDetail> TeacherList { get; set; }
    public List<StudentDetail> StudentList { get; set; }
    public List<SemesterDto> SemesterList { get; set; }
    public List<SubjectDto> SubjectList { get; set; }
    public List<ClassDto> ClassList { get; set; }
    
    
    public void OnGet()
    {
        TeacherList = _teacherDetailRepository.GetAllTeacherDetails().ToList();
        StudentList = _studentDetailRepository.GetAllStudentDetails().ToList();
        SemesterList = _semesterRepository.GetAllSemester().ToList();
        SubjectList = _subjectRepository.GetAllSubjects().ToList();
        ClassList = _classRepository.GetAllClasses().ToList();
    }

    public IActionResult OnPost(int teacherId, int studentId, int semesterId, int subjectId, int classId)
    {
        TeacherList = _teacherDetailRepository.GetAllTeacherDetails().ToList();
        StudentList = _studentDetailRepository.GetAllStudentDetails().ToList();
        SemesterList = _semesterRepository.GetAllSemester().ToList();
        SubjectList = _subjectRepository.GetAllSubjects().ToList();
        ClassList = _classRepository.GetAllClasses().ToList();
        var existed = _enrollmentRepository.GetEnrollmentDetailByStudent(studentId, semesterId, subjectId);
        if (existed != null)
        {
            ViewData["Message"] = "Duplicate";
        }

        var enrollId = _enrollmentRepository.GetNextId();
        var newEnroll = new Enrollment
        {
            TeacherId = teacherId,
            StudentId = studentId,
            SemesterId = semesterId,
            ClassId = classId,
            EnrollmentId = enrollId,
            SubjectId = subjectId
        };
        _enrollmentRepository.AddEnrollment(newEnroll);
        ViewData["Message"] = "Duplicate";
        var subjectDetail = _subjectRepository.GetSubjectDetailById(subjectId);
        foreach (var mark in subjectDetail.MarkList)
        {
            _markReportRepository.AddMarkReport(new MarkReport()
            {
                EnrollmentId = enrollId,
                MarkId = mark.MarkId,
                MarkValue = 0
            });
        }
        return Page();
    }
}