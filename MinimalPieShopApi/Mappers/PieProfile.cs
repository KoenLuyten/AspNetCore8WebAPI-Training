using AutoMapper;

namespace MinimalPieShopApi.Mappers
{
    public class PieProfile: Profile
    {
        public PieProfile()
        {
            CreateMap<Models.Pie, Models.PieDto>();

            CreateMap<Models.Pie, Models.PieForListDto>();

            CreateMap<Models.PieForCreationDto, Models.Pie>();

            CreateMap<Models.PieForUpdateDto, Models.Pie>();
        }
    }
}
