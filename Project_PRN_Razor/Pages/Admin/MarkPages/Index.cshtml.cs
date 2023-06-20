using AutoMapper;
using Domain.Dtos;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MarkPages;

public class Index : PageModel
{
    private IMarkRepository _markRepository;
    private IMapper _mapper;
    public IEnumerable<MarkDto> Marks;

    public Index(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        var markList = _markRepository.GetAllMarks().Where(x => !x.Deleted);
        Marks = _mapper.Map<IEnumerable<MarkDto>>(markList);
    }
}