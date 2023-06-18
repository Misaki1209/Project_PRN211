using AutoMapper;
using Domain.Dtos;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;

public class Index : PageModel
{
    private ISemesterRepository _semesterRepository;
    private IMapper _mapper;
    public IEnumerable<SemesterDto> Semesters;

    public Index(ISemesterRepository semesterRepository, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _mapper = mapper;
    }
    public void OnGet()
    {
        var semesterList = _semesterRepository.GetAllSemester().Where(x => !x.Deleted);
        Semesters = _mapper.Map<IEnumerable<SemesterDto>>(semesterList);
    }
}