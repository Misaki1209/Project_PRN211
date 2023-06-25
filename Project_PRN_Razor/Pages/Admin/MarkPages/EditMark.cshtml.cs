using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MarkPages;

public class EditMark : PageModel
{
    private IMarkRepository _markRepository;
    private IMapper _mapper;
    public MarkDto? Mark { get; set; }
    public IEnumerable<MarkDto> Marks { get; set; }

    public EditMark(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }
    public void OnGet(int markId)
    {
        Mark = _markRepository.GetMarkById(markId);
        Marks = _markRepository.GetAllMarks().Where(x => x.Deleted.Equals(Common.Status.Active));
        return;
    }
    public IActionResult OnPostUpdate(MarkDto mark)
    {
        if (mark.Resit == 0) mark.Resit = null;
        _markRepository.UpdateMark(mark);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(MarkDto mark)
    {
        _markRepository.DeleteMark(mark.MarkId);
        return RedirectToPage("Index");
    }
}