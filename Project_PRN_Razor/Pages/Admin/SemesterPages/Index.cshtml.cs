using AutoMapper;
using Domain.Constants;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN_Razor.Pages.Admin.SemesterPages;
[Authorize(Roles = "Admin")]
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
        var semesterList = _semesterRepository.GetAllSemester().Where(x => x.Deleted.Equals(Common.Status.Active));
        Semesters = _mapper.Map<IEnumerable<SemesterDto>>(semesterList);
    }
}