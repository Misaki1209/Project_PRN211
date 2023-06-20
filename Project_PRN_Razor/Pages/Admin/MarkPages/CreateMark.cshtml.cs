using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MarkPages;

public class CreateMark : PageModel
{
    private IMarkRepository _markRepository;
    private IMapper _mapper;
    public Mark Mark { get; set; }
    public IEnumerable<Mark> Marks { get; set; }

    public CreateMark(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        Marks = _markRepository.GetAllMarks();
    }

    public IActionResult OnPost(Mark mark)
    {
        if (mark.Resit == 0) mark.Resit = null;
        mark.MarkId = _markRepository.GetNextId();
        mark.Deleted = false;
        _markRepository.AddMark(mark);
        return RedirectToPage("Index");
    }
}