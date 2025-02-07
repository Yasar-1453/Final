using AutoMapper;
using Backend.Api.DTO.GameKey;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Interface;

namespace Backend.Api.Services.Implementations
{
    public class GameKeyService : IGameKeyService
    {
        readonly IGameKeyRepository _rep;
        IMapper _mapper;
        public GameKeyService(IGameKeyRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<GetGameKeyDto> CreateAsync(CreateGameKeyDto dto)
        {
            var key = _mapper.Map<GameKey>(dto);
            var newKey = await _rep.Create(key);
            await _rep.SaveChangesAsync();
            return _mapper.Map<GetGameKeyDto>(newKey);
        }

        public async Task Delete(int id)
        {
            var key = await _rep.GetById(id);
            _rep.Delete(_mapper.Map<GameKey>(key));
            await _rep.SaveChangesAsync();
        }

        public List<GetGameKeyDto> GetAll()
        {
            List<GetGameKeyDto> dtos = new();
            var datas = _rep.GetAll();
            foreach (var data in datas)
            {
                GetGameKeyDto dto = _mapper.Map<GetGameKeyDto>(data);
                dtos.Add(dto);

            }
            return dtos;
        }

        public async Task<GetGameKeyDto> GetById(int id)
        {
            var dto = _mapper.Map<GetGameKeyDto>(await _rep.GetById(id));
            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var key = await GetById(id);
            _rep.Delete(_mapper.Map<GameKey>(key));
            await _rep.SaveChangesAsync();
        }

        public async Task Update(UpdateGameKeyDto dto)
        {
            var key = await GetById(dto.Id);
        
            key = _mapper.Map<GetGameKeyDto>(dto);
            _rep.Update(_mapper.Map<GameKey>(key));
            await _rep.SaveChangesAsync();
        }
    }
}
