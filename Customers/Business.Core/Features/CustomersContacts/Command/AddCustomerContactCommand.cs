using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CustomersContacts.Command
{
    public class AddCustomerContactCommand : CustomerContact, IRequest<HttpResponse<CustomerContactsDto>>
    {
       
        
    }

    public class AddCustomerContactsCommandHandler : IRequestHandler<AddCustomerContactCommand,HttpResponse<CustomerContactsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accesor; //consultar sobre este e investigar 
        public AddCustomerContactsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accesor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accesor = accesor;
        }
        public async Task<HttpResponse<CustomerContactsDto>> Handle(AddCustomerContactCommand request,CancellationToken cancellationToken)
        {
            var questionType = _mapper.Map<CustomerContact>(request);
            var result = await _unitOfWork.CustomerContactRepository.AddAsync(questionType);
            var entity = _mapper.Map<CustomerContactsDto>(result);
            _accesor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CustomerContactsDto>(entity);
        }
    }
}
