using AutoMapper;
using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Entities;

namespace LogAndStore.Application.Mapping
{
    public class MyDataMappingProfile : Profile
    {
        public MyDataMappingProfile()
        {
            CreateMap<InputMyDataDto, MyData>()
                .ForMember(dest => dest.Code, opt =>
                    opt.MapFrom(src => SafeParseCode(src.Code)));

            CreateMap<MyData, OutputMyDataDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Id));
        }

        private static int SafeParseCode(string code)
        {
            return int.TryParse(code, out var parsed) ? parsed : 0;
        }
    }
}