using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Positions.Command
{
    public class AddPositionCommand : IRequest<HttpResponse<PositionDto>>
    {
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int CustomerId { get; set; }
    }

    public class AddPositionCommandHandler : IRequestHandler<AddPositionCommand, HttpResponse<PositionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accesor; //consultar sobre este e investigar 
        public AddPositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accesor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accesor = accesor;
        }
        public async Task<HttpResponse<PositionDto>> Handle(AddPositionCommand request, CancellationToken cancellationToken)
        {
            var position = _mapper.Map<Position>(request);
            var result = await _unitOfWork.PositionRepository.Add(position);
            var entity = _mapper.Map<PositionDto>(result);
            _accesor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<PositionDto>(entity);
        }

        
    }
}
