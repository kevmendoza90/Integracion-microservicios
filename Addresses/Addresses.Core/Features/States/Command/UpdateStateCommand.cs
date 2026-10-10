using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.States.Command
{
    public class UpdateStateCommand : CityDto, IRequest<HttpResponse<string>>
    {
    }

    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdateStateCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.StateRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Name = request.Name;
            entity.CountryId= request.CountryId;
            entity.IsActive = request.IsActive;
            await _unitOfWork.StateRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }


    }
}
