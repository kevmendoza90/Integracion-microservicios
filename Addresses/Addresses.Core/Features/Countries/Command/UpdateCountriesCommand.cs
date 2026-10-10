using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Countries.Command
{
    
    public class UpdateCountriesCommand : CountryDto, IRequest<HttpResponse<string>>
    {
    }
    public class UpdateCountriesCommandHandler : IRequestHandler<UpdateCountriesCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdateCountriesCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdateCountriesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CountryRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Name = request.Name;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CountryRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");

        }


    }
}
