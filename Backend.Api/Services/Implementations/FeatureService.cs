using AutoMapper;
using Backend.Api.DTO.Feature;
using Backend.Api.Models;
using Backend.Api.Repositories.Implementations;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Interface;

namespace Backend.Api.Services.Implementations
{
    public class FeatureService : IFeatureService
    {
        readonly IFeaturesRepository _rep;
        IMapper _mapper;

        public FeatureService(IFeaturesRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<GetFeatureDto> CreateAsync(CreateFeatureDto dto)
        {
            var feature = _mapper.Map<Feature>(dto);
            var newFeature = await _rep.Create(feature);
            await _rep.SaveChangesAsync();
            return _mapper.Map<GetFeatureDto>(newFeature);
        }

        public async Task Delete(int id)
        {
            var feature = await GetById(id);
            _rep.Delete(_mapper.Map<Feature>(feature));
            await _rep.SaveChangesAsync();
        }

        public List<GetFeatureDto> GetAll()
        {
            List<GetFeatureDto> dtos = new();
            var datas = _rep.GetAll();
            foreach (var data in datas)
            {
                GetFeatureDto dto = _mapper.Map<GetFeatureDto>(data);
                dtos.Add(dto);

            }
            return dtos;
        }

        public async Task<GetFeatureDto> GetById(int id)
        {
            var dto = _mapper.Map<GetFeatureDto>(await _rep.GetById(id));
            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var feature = await GetById(id);
            _rep.Delete(_mapper.Map<Feature>(feature));
            await _rep.SaveChangesAsync();
        }

        public async Task Update(UpdateFeatureDto dto)
        {
            var feature = await GetById(dto.Id);
           
            feature = _mapper.Map<GetFeatureDto>(dto);
            _rep.Update(_mapper.Map<Feature>(feature));
            await _rep.SaveChangesAsync();
        }
    }
}
