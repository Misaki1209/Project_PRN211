using System.Globalization;
using Domain.Constants;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Mapping;

public class MappingProfile:AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Semester, SemesterDto>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted?Common.Status.Deleted:Common.Status.Active));

        CreateMap<Class, ClassDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));
        CreateMap<Mark, MarkDto>()
            .ForMember(des => des.Deleted,
            act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));;
        CreateMap<Major, MajorDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));;

    }
}