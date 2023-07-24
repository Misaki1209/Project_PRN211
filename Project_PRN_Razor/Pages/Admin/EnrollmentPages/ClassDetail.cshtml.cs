using System.Security.Claims;
using AutoMapper;
using ClosedXML.Excel;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.EnrollmentPages;

[Authorize(Roles = "Admin")]
public class ClassDetail : PageModel
{
    private ISemesterRepository _semesterRepository;
    private ISubjectRepository _subjectRepository;
    private IClassRepository _classRepository;
    private IEnrollmentRepository _enrollmentRepository;
    private IMarkReportRepository _markReportRepository;
    private IMarkRepository _markRepository;
    
    [BindProperty]
    public SemesterDto? SelectedSemester { get; set; }
    [BindProperty]
    public SubjectDto? SelectedSubject { get; set; }
    [BindProperty]
    public ClassDto? SelectedClass { get; set; }
    
    private IMapper _mapper;

    [BindProperty]
    public List<EnrollmentForClassList> EnrollmentList { get; set; }
    
    [BindProperty]
    public List<List<MarkReportDto>> MarkReportList { get; set; }

    [BindProperty]
    public List<string> MarkNameList { get; set; }
    public ClassDetail(ISemesterRepository semesterRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, IEnrollmentRepository enrollmentRepository, IMarkReportRepository markReportRepository, IMarkRepository markRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _subjectRepository = subjectRepository;
        _classRepository = classRepository;
        _enrollmentRepository = enrollmentRepository;
        _markReportRepository = markReportRepository;
        _markRepository = markRepository;
        _mapper = mapper;
    }
    
    public void OnGet(int selectedSemesterId, int selectedSubjectId, int selectedClassId)
    {
        SelectedClass = _classRepository.GetClassById(selectedClassId);
        SelectedSemester = _semesterRepository.GetSemesterById(selectedSemesterId);
        SelectedSubject = _subjectRepository.GetSubjectById(selectedSubjectId);
        EnrollmentList = _enrollmentRepository.GetClassDetailByAdmin( selectedSemesterId, selectedSubjectId, selectedClassId);
        MarkReportList = new List<List<MarkReportDto>>();
        for (var i = 0; i < EnrollmentList.Count; i++)
        {
            var markList = _markReportRepository.GetMarkReportByEnrollmentId(EnrollmentList[i].EnrollmentId);
            MarkReportList.Add(markList.Select(x => new MarkReportDto
            {
                MarkId = x.MarkId,
                MarkName = x.Mark.MarkName,
                MarkValue = x.MarkValue,
                Coefficient = x.Mark.Coefficient * 100 + "%"
            }).ToList());
        }

        MarkNameList = new List<string>();
        var subjectDetail = _subjectRepository.GetSubjectDetailById(SelectedSubject.SubjectId);
        foreach (var mark in subjectDetail.MarkList)
        {
            MarkNameList.Add(mark.Mark.MarkName);
        }
    }

    public IActionResult OnPostGetTemplate(int selectedSemesterId, int selectedSubjectId, int selectedClassId)
    {
        SelectedClass = _classRepository.GetClassById(selectedClassId);
        SelectedSemester = _semesterRepository.GetSemesterById(selectedSemesterId);
        SelectedSubject = _subjectRepository.GetSubjectById(selectedSubjectId);
        EnrollmentList = _enrollmentRepository.GetClassDetail(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), selectedSemesterId, selectedSubjectId, selectedClassId);

        var workBook = BuildTemplate();
        using MemoryStream streamWriter = new MemoryStream();
        workBook.SaveAs(streamWriter);

        return File(streamWriter.ToArray(),  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "templateMark.xlsx");
        //return Page();
    }
    

    private XLWorkbook BuildTemplate()
    {
        var subjectDetail = _subjectRepository.GetSubjectDetailById(SelectedSubject.SubjectId);

        var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Mark Report");

        int row = 1, column = 1;
        
        ws.Cell(row, column).Value = "Student Code";
        column++;

        ws.Cell(row, column).Value = "Student name";
        column++;

        foreach (var mark in subjectDetail.MarkList)
        {
            ws.Cell(row, column).Value = mark.Mark.MarkName;
            column++;
        }

        foreach (var enroll in EnrollmentList)
        {
            var markDetailList = _markReportRepository.GetMarkReportByEnrollmentId(enroll.EnrollmentId);
            row++;
            column = 1;
            ws.Cell(row, column).Value = enroll.StudentCode;
            column++;
            
            ws.Cell(row, column).Value = enroll.StudentName;
            column++;

            foreach (var mark in markDetailList)
            {
                ws.Cell(row, column).Value = mark.MarkValue;
                column++;
            }

        }

        return workbook;
    }

    

    
}