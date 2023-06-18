using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;

public class CreateSemester : PageModel
{
    private ISemesterRepository _semesterRepository;
    private IMapper _mapper;
    public Semester Semester { get; set; }

    public CreateSemester(ISemesterRepository semesterRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(Semester semester)
    {
        semester.SemesterId = _semesterRepository.GetNextId();
        semester.Deleted = false;
        _semesterRepository.AddSemester(semester);
        return RedirectToPage("Index");
    }
}