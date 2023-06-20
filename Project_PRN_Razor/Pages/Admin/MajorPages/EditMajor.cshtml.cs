using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MajorPages;

public class EditMajor : PageModel
{
    private IMajorRepository _majorRepository;
    private IMapper _mapper;
    public Major? Major { get; set; }

    public EditMajor(IMajorRepository majorRepository, IMapper mapper)
    {
        _majorRepository = majorRepository;
        _mapper = mapper;
    }
    public void OnGet(int majorId)
    {
        Major = _majorRepository.GetMajorById(majorId);
    }
    public IActionResult OnPostUpdate(Major major)
    {
        _majorRepository.UpdateMajor(major);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(Major major)
    {
        _majorRepository.DeleteMajor(major.MajorId);
        return RedirectToPage("Index");
    }
}