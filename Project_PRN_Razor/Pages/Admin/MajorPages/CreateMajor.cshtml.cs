using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MajorPages;

public class CreateMajor : PageModel
{
    private IMajorRepository _majorRepository;
    private IMapper _mapper;
    public MajorDto Major { get; set; }

    public CreateMajor(IMajorRepository majolrRepository, IMapper mapper)
    {
        _majorRepository = _majorRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(MajorDto major)
    {
        major.MajorId = _majorRepository.GetNextId();
        major.Deleted = Common.Status.Active;
        _majorRepository.AddMajor(major);
        return RedirectToPage("Index");
    }
}