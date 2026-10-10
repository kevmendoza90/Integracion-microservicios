using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Positions.Command
{
    public class UpdatePositionCommand : PositionDto, IRequest<HttpResponse<string>>
    {
    }

    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdatePositionCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.PositionRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.ZipCode = request.ZipCode;
            entity.CityId = request.CityId;
            entity.Address = request.Address;
            entity.Latitude = request.Latitude;
            entity.Longitude = request.Longitude;
            entity.CustomerId = request.CustomerId;
            entity.IsActive = request.IsActive;
            await _unitOfWork.PositionRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }


    }

}
