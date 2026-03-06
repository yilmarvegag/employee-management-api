using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
           .ForMember(
               dest => dest.CurrentPosition,
               opt => opt.MapFrom(src => src.CurrentPosition.ToString())
           )
           .ForMember(
                dest => dest.Salary,
                opt => opt.MapFrom(src => Math.Round(src.Salary, 2))
            );

            CreateMap<EmployeeDto, Employee>();
        }
    }
}
