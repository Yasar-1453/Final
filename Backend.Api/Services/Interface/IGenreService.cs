using Backend.Api.DTO.Feature;
using Backend.Api.DTO.Genre;

namespace Backend.Api.Services.Interface
{
    public interface IGenreService
    {
        Task<GetGenreDto> CreateAsync(CreateGenreDto dto);
        Task<GetGenreDto> GetById(int id);
        List<GetGenreDto> GetAll();
        Task Update(UpdateGenreDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
