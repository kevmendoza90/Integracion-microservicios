using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CutomersTypes.Command
{

    /// <summary>
    /// El Id se es recuperado de FromRoute y se es enviado dentro del command y luego bloqueado con BindNever para no ser editado
    /// </summary>
    
    
    public class UpdateCustomersTypesCommand : CustomerTypeDto, IRequest<HttpResponse<string>>
    {
        
    }
    //public class UpdateCustomersTypesCommandHandler : IRequestHandler<UpdateCustomersTypesCommand, HttpResponse<string>>
    public class UpdateCustomersTypesCommandHandler : IRequestHandler<UpdateCustomersTypesCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;//agregado
        public UpdateCustomersTypesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;//agregado
        }
        
        public async Task<HttpResponse<string>> Handle(UpdateCustomersTypesCommand request, CancellationToken cancellationToken)
        {

            var entity = await _unitOfWork.CustomerTypeRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Name = request.Name;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CustomerTypeRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }

    }
}
