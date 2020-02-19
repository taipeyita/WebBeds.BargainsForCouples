using AutoMapper;
using BargainsForCouples.MicroService.Dto;
using BargainsForCouples.MicroService.Model;

namespace BargainsForCouples.MicroService.MappingConfigurations
{
    /// <summary>
    /// Property Auto Mapper configuration
    /// </summary>
    public class PropertyMapper : Profile
    {

        public PropertyMapper()
        {
            CreateMap<Property, PropertyBO>();
            CreateMap<Hotel, HotelBO>();
            CreateMap<Rate, RateBO>().ForMember(a=> a.FinalPrice, b=> b.MapFrom(y=> y.Value) );
        }
    }
}
