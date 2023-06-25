using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;

public class CreateSemester : PageModel
{
    private ISemesterRepository _semesterRepository;
    private IMapper _mapper;
    public SemesterDto Semester { get; set; }

    public CreateSemester(ISemesterRepository semesterRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(SemesterDto semester)
    {
        semester.SemesterId = _semesterRepository.GetNextId();
        semester.Deleted = Common.Status.Active;
        _semesterRepository.AddSemester(semester);
        return RedirectToPage("Index");
    }
}