using AutoMapper;
using Backend.Api.DTO.Game;
using Backend.Api.Helpers.Extensions;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Interface;

namespace Backend.Api.Services.Implementations
{
    public class GameService : IGameService
    {
        readonly IGameRepository _rep;
        IWebHostEnvironment _env;
        IMapper _mapper;
        public GameService(IGameRepository rep, IMapper mapper, IWebHostEnvironment env)
        {
            _rep = rep;
            _mapper = mapper;
            _env = env;
        }
        public async Task<GetGameDto> CreateAsync(CreateGameDto dto)
        {
            dto.ImageUrl = dto.Photo.Upload(_env.WebRootPath, "Image/Game");

            var game = _mapper.Map<Game>(dto);
            var newGame = await _rep.Create(game);
            await _rep.SaveChangesAsync();
            return _mapper.Map<GetGameDto>(newGame);
        }

        public async Task Delete(int id)
        {
            var game = await _rep.GetById(id);
            _rep.Delete(_mapper.Map<Game>(game));
            await _rep.SaveChangesAsync();
        }

        public List<GetGameDto> GetAll()
        {
            List<GetGameDto> dtos = new();
            var datas = _rep.GetAll();
            foreach (var data in datas)
            {
                GetGameDto dto = _mapper.Map<GetGameDto>(data);
                dtos.Add(dto);

            }
            return dtos;
        }

        public async Task<GetGameDto> GetById(int id)
        {
       
            var dto = _mapper.Map<GetGameDto>(await _rep.GetById(id));
            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var game = await GetById(id);
            _rep.Delete(_mapper.Map<Game>(game));
            await _rep.SaveChangesAsync();
        }

        public async Task Update(UpdateGameDto dto)
        {
            var oldGame = await GetById(dto.Id);

            oldGame = _mapper.Map<GetGameDto>(dto);
            _rep.Update(_mapper.Map<Game>(oldGame));
            await _rep.SaveChangesAsync();
        }
    }
}
