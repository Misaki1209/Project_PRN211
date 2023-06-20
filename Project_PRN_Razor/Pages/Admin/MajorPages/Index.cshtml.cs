using AutoMapper;
using Domain.Dtos;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.MajorPages;

public class Index : PageModel
{
    private IMajorRepository _majorRepository;
    private IMapper _mapper;
    public IEnumerable<MajorDto> Majors;

    public Index(IMajorRepository majorRepository, IMapper mapper)
    {
        _majorRepository = majorRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        var majorList = _majorRepository.GetAllMajors().Where(x => !x.Deleted);
        Majors = _mapper.Map<IEnumerable<MajorDto>>(majorList);
    }
}