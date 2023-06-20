using AutoMapper;
using Domain.Dtos;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.ClassPages;

public class Index : PageModel
{
    private IClassRepository _classRepository;
    private IMapper _mapper;
    public IEnumerable<ClassDto> Classes;

    public Index(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        var classList = _classRepository.GetAllClasses().Where(x => !x.Deleted);
        Classes = _mapper.Map<IEnumerable<ClassDto>>(classList);
    }
}