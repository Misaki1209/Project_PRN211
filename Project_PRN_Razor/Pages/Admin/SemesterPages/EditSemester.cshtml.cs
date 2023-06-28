using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;
[Authorize(Roles = "Admin")]
public class EditSemester : PageModel
{
    private ISemesterRepository _semesterRepository;
    private IMapper _mapper;
    public SemesterDto? Semester { get; set; }

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
    public IActionResult OnPostUpdate(SemesterDto semester)
    {
        _semesterRepository.UpdateSemester(semester);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(SemesterDto semester)
    {
        _semesterRepository.DeleteSemester(semester.SemesterId);
        return RedirectToPage("Index");
    }
}