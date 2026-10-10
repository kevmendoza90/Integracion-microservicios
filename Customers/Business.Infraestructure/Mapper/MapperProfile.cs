using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Features.Customers.Command;
using Business.Core.Features.CutomersTypes.Command;
using Business.Domain.Models;

namespace Business.Infraestructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCustomerCommand, AddressDto>()
                .ForMember(d => d.ZipCode, m => m.MapFrom(s => s.AddressDto.ZipCode))
                .ForMember(d => d.Address, m => m.MapFrom(s => s.AddressDto.Address))
                .ForMember(d => d.CityId, m => m.MapFrom(s =>  s.AddressDto.CityId))
                .ForMember(d => d.CustomerId, m => m.MapFrom(s => s.AddressDto.CustomerId))
                .ForMember(d => d.Latitude, m => m.MapFrom(s => s.AddressDto.Latitude))
                .ForMember(d => d.Longitude, m => m.MapFrom(s => s.AddressDto.Longitude));

        }
    }
}
