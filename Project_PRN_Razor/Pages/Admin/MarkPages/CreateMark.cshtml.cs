using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MarkPages;

public class CreateMark : PageModel
{
    private IMarkRepository _markRepository;
    private IMapper _mapper;
    public MarkDto Mark { get; set; }
    public IEnumerable<MarkDto> Marks { get; set; }

    public CreateMark(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        Marks = _markRepository.GetAllMarks().Where(x => x.Deleted.Equals(Common.Status.Active));
    }

    public IActionResult OnPost(MarkDto mark)
    {
        if (mark.Resit == 0) mark.Resit = null;
        mark.MarkId = _markRepository.GetNextId();
        mark.Deleted = Common.Status.Active;
        _markRepository.AddMark(mark);
        return RedirectToPage("Index");
    }
}