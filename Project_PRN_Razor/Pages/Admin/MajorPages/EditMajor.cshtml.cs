using AutoMapper;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MajorPages;
[Authorize(Roles = "Admin")]
public class EditMajor : PageModel
{
    private IMajorRepository _majorRepository;
    private IMapper _mapper;
    public MajorDto? Major { get; set; }

    public EditMajor(IMajorRepository majorRepository, IMapper mapper)
    {
        _majorRepository = majorRepository;
        _mapper = mapper;
    }
    public void OnGet(int majorId)
    {
        Major = _majorRepository.GetMajorById(majorId);
    }
    public IActionResult OnPostUpdate(MajorDto major)
    {
        _majorRepository.UpdateMajor(major);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(MajorDto major)
    {
        _majorRepository.DeleteMajor(major.MajorId);
        return RedirectToPage("Index");
    }
}