using AutoMapper;
using VentanillaUnica.Tramites.Dtos;

namespace VentanillaUnica.Tramites.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Parameter, ParameterDto>();
            CreateMap<ParameterDto, Domain.Entities.Parameter>();
        }
    }
}
