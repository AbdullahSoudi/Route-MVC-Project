using AutoMapper;
using Route.BLL.DTOs.Department;
using Route.PL.ViewModels.DepartmentViewModels;

namespace Route.PL.Mapping
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<DepartmentViewModel, DepartmentCreationDTO>();
            CreateMap<DepartmentViewModel, DepartmentUpdateDTO>();
            CreateMap<DepartmentDetailsDTO, DepartmentViewModel>();
        }
    }
}
