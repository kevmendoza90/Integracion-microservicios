using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Countries.Command
{
    
    public class AddCountriesCommand : IRequest<HttpResponse<CountryDto>>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddCountriesCommandHandler : IRequestHandler<AddCountriesCommand, HttpResponse<CountryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public AddCountriesCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<CountryDto>> Handle(AddCountriesCommand request, CancellationToken cancellationToken)
        {
            var questionType = _mapper.Map<Country>(request);
            var result = await _unitOfWork.CountryRepository.AddAsync(questionType);
            var entity = _mapper.Map<CountryDto>(result);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CountryDto>(entity);
        }
    }
}
