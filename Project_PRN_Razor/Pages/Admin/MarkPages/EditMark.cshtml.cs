using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MarkPages;

public class EditMark : PageModel
{
    private IMarkRepository _markRepository;
    private IMapper _mapper;
    public Mark? Mark { get; set; }
    public IEnumerable<Mark> Marks { get; set; }

    public EditMark(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }
    public void OnGet(int markId)
    {
        Mark = _markRepository.GetMarkById(markId);
        Marks = _markRepository.GetAllMarks();
        return;
    }
    public IActionResult OnPostUpdate(Mark mark)
    {
        if (mark.Resit == 0) mark.Resit = null;
        _markRepository.UpdateMark(mark);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostDelete(Mark mark)
    {
        _markRepository.DeleteMark(mark.MarkId);
        return RedirectToPage("Index");
    }
}