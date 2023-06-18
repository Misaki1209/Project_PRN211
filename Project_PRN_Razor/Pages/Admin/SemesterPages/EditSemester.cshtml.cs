using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;

public class EditSemester : PageModel
{
    private ISemesterRepository _semesterRepository;
    private IMapper _mapper;
    public Semester? Semester { get; set; }

    public EditSemester(ISemesterRepository semesterRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _mapper = mapper;
    }
    public void OnGet(int semesterId)
    {
        Semester = _semesterRepository.GetSemesterById(semesterId);
        return;
    }
    public IActionResult OnPostUpdate(Semester semester)
    {
        _semesterRepository.UpdateSemester(semester);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(Semester semester)
    {
        _semesterRepository.DeleteSemester(semester.SemesterId);
        return RedirectToPage("Index");
    }
}