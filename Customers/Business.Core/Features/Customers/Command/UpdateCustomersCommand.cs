using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Customers.Command
{
    public class UpdateCustomersCommand  : CustomerDto,IRequest<HttpResponse<string>>
    {
        
    }

    public class UpdateCostumersCommandHandler : IRequestHandler<UpdateCustomersCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
    public UpdateCostumersCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;

        }
        public async Task<HttpResponse<string>> Handle(UpdateCustomersCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CustomerRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Code = request.Code;
            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.CustomerTypeId = request.CustomerTypeId;
            entity.CompanyId = request.CompanyId;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CustomerRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");



        }

        
    }
}
