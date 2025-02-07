using AutoMapper;
using Backend.Api.DTO.Feature;
using Backend.Api.Models;

namespace Backend.Api.Helpers.Mappers
{
    public class FeatureMapper : Profile
    {
        public FeatureMapper()
        {

            CreateMap<CreateFeatureDto, Feature>().ReverseMap();
            CreateMap<GetFeatureDto, Feature>().ReverseMap();
            CreateMap<GetAllFeatureDto, Feature>().ReverseMap();
            CreateMap<UpdateFeatureDto, GetFeatureDto>().ReverseMap();
        }
    }
}
