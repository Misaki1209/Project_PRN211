using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.ClassPages;

public class EditClass : PageModel
{
    private IClassRepository _classRepository;
    private IMapper _mapper;
    public Class? TheClass { get; set; }

    public EditClass(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }
    public void OnGet(int classId)
    {
        TheClass = _classRepository.GetClassById(classId);
        return;
    }
    public IActionResult OnPostUpdate(Class theClass)
    {
        _classRepository.UpdateClass(theClass);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(Class theClass)
    {
        _classRepository.DeleteClass(theClass.ClassId);
        return RedirectToPage("Index");
    }
}