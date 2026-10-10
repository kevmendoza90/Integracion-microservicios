using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Cities.Command
{
    public class UpdateCitiesCommand : CityDto, IRequest<HttpResponse<string>>
    {
    }

    public class UpdateCitiesCommandHandler : IRequestHandler<UpdateCitiesCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdateCitiesCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdateCitiesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CityRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Name = request.Name;
            entity.CountryId= request.CountryId;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CityRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }


    }
}
