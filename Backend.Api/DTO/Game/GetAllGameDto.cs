using Backend.Api.DTO.Feature;

namespace Backend.Api.DTO.Game
{
    public class GetAllGameDto
    {
        public IQueryable<GetGameDto> Games { get; set; }

    }
}
