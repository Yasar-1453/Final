using Backend.Api.DTO.Feature;

namespace Backend.Api.DTO.Genre
{
    public class GetAllGenreDto
    {
        public IQueryable<GetGenreDto> Genres { get; set; }

    }
}
