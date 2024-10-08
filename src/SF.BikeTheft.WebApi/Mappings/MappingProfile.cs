using AutoMapper;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.BikeTheft, BikeDto>().ReverseMap();
        CreateMap<BikeDto, Domain.Entities.BikeEntity>().ReverseMap();
    }
}
