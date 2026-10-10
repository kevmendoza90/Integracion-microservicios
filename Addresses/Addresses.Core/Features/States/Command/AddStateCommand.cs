using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.States.Command
{

    public class AddStateCommand : IRequest<HttpResponse<StateDto>>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddStateTypeCommandHandler : IRequestHandler<AddStateCommand, HttpResponse<StateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public AddStateTypeCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<StateDto>> Handle(AddStateCommand request, CancellationToken cancellationToken)
        {
            var questionType = _mapper.Map<State>(request);
            var result = await _unitOfWork.StateRepository.AddAsync(questionType);
            var entity = _mapper.Map<StateDto>(result);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<StateDto>(entity);
        }
    }

}
