using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.ClassPages;

public class CreateClass : PageModel
{
    private IClassRepository _classRepository;
    private IMapper _mapper;
    public Class TheClass { get; set; }

    public CreateClass(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(Class theClass)
    {
        theClass.ClassId = _classRepository.GetNextId();
        theClass.Deleted = false;
        _classRepository.AddClass(theClass);
        return RedirectToPage("Index");
    }
}