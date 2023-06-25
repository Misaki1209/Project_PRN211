using System.Globalization;
using Domain.Constants;
using Infrastructure.Dtos;
using Domain.Models;

namespace Infrastructure.Mapping;

public class MappingProfile:AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Semester, SemesterDto>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted?Common.Status.Deleted:Common.Status.Active));
        CreateMap<SemesterDto, Semester>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted.Equals(Common.Status.Deleted)));

        
        CreateMap<Class, ClassDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));
        CreateMap<ClassDto, Class>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted.Equals(Common.Status.Deleted)));
        
        
        CreateMap<Mark, MarkDto>()
            .ForMember(des => des.Deleted,
            act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));
        CreateMap<MarkDto, Mark>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted.Equals(Common.Status.Deleted)));
        
        
        CreateMap<Major, MajorDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active));
        CreateMap<MajorDto, Major>()
            .ForMember(des => des.Deleted, 
                act => act.MapFrom(src => src.Deleted.Equals(Common.Status.Deleted)));


        CreateMap<Subject, SubjectDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active))
            .ForMember(des => des.MarkAvailable,
                act => act.MapFrom(src => src.MarkAvailable ? Common.AvailableStatus.Available : Common.AvailableStatus.UnAvailable));
        CreateMap<SubjectDto, Subject>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted.Equals(Common.Status.Deleted)))
            .ForMember(des => des.MarkAvailable,
                act => act.MapFrom(src => src.MarkAvailable.Equals(Common.AvailableStatus.Available)));
        CreateMap<Subject, SubjectDetailDto>()
            .ForMember(des => des.Deleted,
                act => act.MapFrom(src => src.Deleted ? Common.Status.Deleted : Common.Status.Active))
            .ForMember(des => des.MarkAvailable,
                act => act.MapFrom(src => src.MarkAvailable ? Common.AvailableStatus.Available : Common.AvailableStatus.UnAvailable))
            .ForMember(des => des.MarkList,
                act => act.MapFrom(src => src.SubjectMarks.ToList()));

    }
}