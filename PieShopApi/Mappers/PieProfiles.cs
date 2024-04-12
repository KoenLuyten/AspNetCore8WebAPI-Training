using AutoMapper;

namespace PieShopApi.Mappers
{
    public class PieProfile : Profile
    {
        public PieProfile()
        {
            CreateMap<Models.Pies.Pie, Models.Pies.PieDto>()
                .ForMember(dest => dest.AllergyItems, opt =>
                    opt.MapFrom(src => src.AllergyItems.Select(a => a.Name).ToList()));

            CreateMap<Models.Pies.Pie, Models.Pies.PieForListDto>();

            CreateMap<Models.Pies.PieForCreationDto, Models.Pies.Pie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AllergyItems, opt => opt.Ignore());

            CreateMap<Models.Pies.PieForUpdateDto, Models.Pies.Pie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AllergyItems, opt => opt.Ignore());
        }
    }
}
