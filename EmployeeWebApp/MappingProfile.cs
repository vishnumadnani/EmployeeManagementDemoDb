using AutoMapper;
using Employee.Domain.Employees;
using EmployeeWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeWebApp;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMapping();
    }

    private void CreateMapping()
    {
        CreateMap<EmpAddRequest, Emp>();
        CreateMap<Emp, EmpAddRequest>()
            .ForMember(dest => dest.EmpSkills, source => source.MapFrom(dest => GetAllSkills(dest.Skills)));
    }

   private string GetAllSkills(List<SkillsModel> Skills)
    {
        if(Skills != null && Skills.Count > 0)
        {
            var skills = string.Join(',', Skills.Select(m => m.Name).ToList());
            return skills;
        }
        return "";
    }
}