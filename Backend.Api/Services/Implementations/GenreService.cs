using AutoMapper;
using Backend.Api.DTO.Feature;
using Backend.Api.DTO.Genre;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Interface;

namespace Backend.Api.Services.Implementations
{
    public class GenreService : IGenreService
    {
        readonly IGenreRepository _rep;
        IMapper _mapper;

        public GenreService(IGenreRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<GetGenreDto> CreateAsync(CreateGenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            var newGenre = await _rep.Create(genre);
            await _rep.SaveChangesAsync();
            return _mapper.Map<GetGenreDto>(newGenre);
        }

        public async Task Delete(int id)
        {
            var genre = await GetById(id);
            _rep.Delete(_mapper.Map<Genre>(genre));
            await _rep.SaveChangesAsync();
        }

        public List<GetGenreDto> GetAll()
        {
            List<GetGenreDto> dtos = new();
            var datas = _rep.GetAll();
            foreach (var data in datas)
            {
                GetGenreDto dto = _mapper.Map<GetGenreDto>(data);
                dtos.Add(dto);

            }
            return dtos;
        }

        public async Task<GetGenreDto> GetById(int id)
        {
            var dto = _mapper.Map<GetGenreDto>(await _rep.GetById(id));
            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var genre = await GetById(id);
            _rep.Delete(_mapper.Map<Genre>(genre));
            await _rep.SaveChangesAsync();
        }

        public async Task Update(UpdateGenreDto dto)
        {
            var feature = await GetById(dto.Id);

            feature = _mapper.Map<GetGenreDto>(dto);
            _rep.Update(_mapper.Map<Genre>(feature));
            await _rep.SaveChangesAsync();
        }

    
    }
}
