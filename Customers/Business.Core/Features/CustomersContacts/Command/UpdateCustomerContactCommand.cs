using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CustomersContacts.Command
{
            
    public class UpdateCustomerContactCommand : CustomerContactsDto, IRequest<HttpResponse<string>>
    {
        
    }
    public class UpdateCustomerContactCommandHandler : IRequestHandler<UpdateCustomerContactCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdateCustomerContactCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CustomerContactRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email;
            entity.Phone1 = request.Phone1;
            entity.Phone2 = request.Phone2;
            entity.CustomerId = request.CustomerId;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CustomerContactRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }


    }
}
