using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Features.Companies.Command;
using Business.Core.Features.Customers.Command;
using Business.Core.Features.CustomersContacts.Command;
using Business.Core.Features.CutomersTypes.Command;
using Business.Domain.Models;

namespace Business.Persistence.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
           

            CreateMap<AddCompaniesCommand, Company>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Sigla, m => m.MapFrom(s => s.Sigla))
                .ForMember(d => d.MainEmail, m => m.MapFrom(s => s.MainEmail));

            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<UpdateCompaniesCommand, Company>()
                .ForMember(d=>d.Id, m=>m.MapFrom(s=>s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Sigla, m => m.MapFrom(s => s.Sigla))
                .ForMember(d => d.MainEmail, m => m.MapFrom(s => s.MainEmail));

            CreateMap<AddCustomerCommand, Customer>()
                .ForMember(d => d.Code, m => m.MapFrom(s => s.Code))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                .ForMember(d => d.CustomerTypeId, m => m.MapFrom(s => s.CustomerTypeId))
                .ForMember(d => d.CompanyId, m => m.MapFrom(s => s.CompanyId));


            //responses
            CreateMap<CustomerType, CustomerTypeDto>().ReverseMap();
            CreateMap<CustomerContact, CustomerContactsDto>().ReverseMap();


            //request
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<AddCustomerContactCommand, CustomerContact>().ReverseMap();
            CreateMap<AddCustomersTypesCommand, CustomerType>().ReverseMap();
            CreateMap<UpdateCustomersTypesCommand, CustomerType>().ReverseMap();
            CreateMap<UpdateCustomerContactCommand, CustomerContact>().ReverseMap();
            CreateMap<UpdateCustomersCommand, Customer>().ReverseMap();





        }

    }
}
