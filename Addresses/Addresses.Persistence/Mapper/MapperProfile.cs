using Addresses.Core.Dtos;
using Addresses.Core.Features.Cities.Command;
using Addresses.Core.Features.Countries.Command;
using Addresses.Core.Features.Positions.Command;
using Addresses.Core.Features.States.Command;
using Addresses.Domain.Models;
using AutoMapper;


namespace Addresses.Persistence.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<AddCitiesCommand, City>().ReverseMap();

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<AddCountriesCommand, Country>().ReverseMap();

            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<AddPositionCommand, Position>().ReverseMap();

            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<AddStateCommand, State>().ReverseMap();


        }
    }
}
