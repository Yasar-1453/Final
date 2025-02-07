namespace Backend.Api.DTO.Feature
{
    public class GetAllFeatureDto
    {
        public IQueryable<GetFeatureDto> Features { get; set; }
    }
}
