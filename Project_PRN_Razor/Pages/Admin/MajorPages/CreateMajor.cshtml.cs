using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MajorPages;

public class CreateMajor : PageModel
{
    private IMajorRepository _majorRepository;
    private IMapper _mapper;
    public Major Major { get; set; }

    public CreateMajor(IMajorRepository majolrRepository, IMapper mapper)
    {
        _majorRepository = _majorRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(Major major)
    {
        major.MajorId = _majorRepository.GetNextId();
        major.Deleted = false;
        _majorRepository.AddMajor(major);
        return RedirectToPage("Index");
    }
}