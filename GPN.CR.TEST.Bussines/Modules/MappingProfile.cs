using AutoMapper;
using GPN.CR.TEST.Bussines.Models.DTO;
using GPN.CR.TEST.Data.Models;

namespace GPN.CR.TEST.Bussines.Modules
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CurrencyDaily, CurrencyDailyDTO>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.CurrencyExtId, m => m.MapFrom(s => s.Currency.ExtId))
                .ForMember(d => d.Date, m => m.MapFrom(s => s.Date))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Value))
                .ForMember(d => d.CurrencyCode, m => m.MapFrom(s => s.Currency.Code))
                .ForMember(d => d.CurrencyName, m => m.MapFrom(s => s.Currency.Name));


            CreateMap<CurrencyDailyDTO, CurrencyDaily>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Date, m => m.MapFrom(s => s.Date))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Value))
                .ForMember(d => d.Currency, m => m.Ignore());
        }
    }
}
