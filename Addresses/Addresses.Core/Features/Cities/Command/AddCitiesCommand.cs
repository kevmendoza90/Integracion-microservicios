using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Cities.Command
{
    
    public class AddCitiesCommand : IRequest<HttpResponse<CityDto>>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddQuestionTypeCommandHandler : IRequestHandler<AddCitiesCommand, HttpResponse<CityDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public AddQuestionTypeCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<CityDto>> Handle(AddCitiesCommand request, CancellationToken cancellationToken)
        {
            var questionType = _mapper.Map<City>(request);
            var result = await _unitOfWork.CityRepository.AddAsync(questionType);
            var entity = _mapper.Map<CityDto>(result);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CityDto>(entity);
        }
    }

}
