using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.ClassPages;

public class CreateClass : PageModel
{
    private IClassRepository _classRepository;
    private IMapper _mapper;
    public ClassDto TheClass { get; set; }

    public CreateClass(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(ClassDto theClass)
    {
        theClass.ClassId = _classRepository.GetNextId();
        theClass.Deleted = Common.Status.Active;
        _classRepository.AddClass(theClass);
        return RedirectToPage("Index");
    }
}