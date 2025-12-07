using AutoMapper;
using PMSystem.Application.DTOs.Companys;
using PMSystem.Domain.Entities;

namespace PMSystem.Application.Mappings
{
    public class CompanyMappingProfile : Profile
    {
      public  CompanyMappingProfile()
        {
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<Company, CompanyDto>()
                .ForMember(des=>des.ProjectsCount,src=>src.MapFrom(otp=>otp.Projects.Count()))
                .ForMember(des => des.UsersCount, src => src.MapFrom(otp => otp.Users.Count()));

            CreateMap<UpdateCompanyDto, Company>();
        }
    }
}
