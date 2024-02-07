using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.DTO;
using WebApi.Domain.Model;

namespace WebApi.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping() 
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name, m => m.MapFrom(orig => orig.name));
        }
    }
}
