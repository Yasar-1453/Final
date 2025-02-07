using Backend.Api.DTO.Feature;

namespace Backend.Api.Services.Interface
{
    public interface IFeatureService
    {
        Task<GetFeatureDto> CreateAsync(CreateFeatureDto dto);
        Task<GetFeatureDto> GetById(int id);
        List<GetFeatureDto> GetAll();
        Task Update(UpdateFeatureDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
