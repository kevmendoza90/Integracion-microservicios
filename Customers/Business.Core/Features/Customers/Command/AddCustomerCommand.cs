using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Core.Interfaces.Services;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Customers.Command
{

    public class AddCustomerCommand : CustomerDto, IRequest<HttpResponse<CustomerDto>>
    {

    }

    public class AddCustomerCommandHanlder: IRequestHandler<AddCustomerCommand, HttpResponse<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAddressService _addressService;
        public AddCustomerCommandHanlder(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IAddressService addressService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
            _addressService = addressService;
        }

        public async Task<HttpResponse<CustomerDto>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Guardar el cliente en Customers
            var entity = _mapper.Map<Customer>(request);
            entity.LocationId = null;
            entity = await _unitOfWork.CustomerRepository.AddAsync(entity);

            // 2. Crear su dirección en Addresses usando la librería REST (AddressService)
            var addressDto = _mapper.Map<AddressDto>(request);
            addressDto.CustomerId = entity.Id;
            var result = await _addressService.AddAsync(addressDto);

            if (!result.Succeeded)
            {
                // Si Addresses rechaza la dirección, se deshace el cliente
                await _unitOfWork.CustomerRepository.RemoveAsync(entity);
                _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new HttpResponse<CustomerDto>(null)
                {
                    Succeeded = false,
                    ValidationErrors = result.ValidationErrors,
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            // 3. Guardar el Id que devolvió Addresses como LocationId
            entity.LocationId = result.Result.Id;
            await _unitOfWork.CustomerRepository.UpdateAsync(entity);

            request.Id = entity.Id;
            request.LocationId = entity.LocationId;
            request.IsActive = true;
            request.AddressDto.Id = result.Result.Id;
            request.AddressDto.CustomerId = entity.Id;
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CustomerDto>(request);
        }

    }

    

}
