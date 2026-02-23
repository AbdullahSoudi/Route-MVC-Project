using AutoMapper;
using Route.BLL.DTOs.Employee;
using Route.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<Employee, EmployeeDetailsDTO>()
                .ForMember(dest => dest.EmpGender, op => op.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmpType, op => op.MapFrom(src => src.EmployeeType));

            CreateMap<EmployeeCreationDTO, Employee>();

            CreateMap<EmployeeUpdateDTO, Employee>().ReverseMap();
        }
    }
}
