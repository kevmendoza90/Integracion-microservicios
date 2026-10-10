using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Positions.Command
{
    public class PartialUpdatePositionCommand : IRequest<HttpResponse<string>>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public JsonPatchDocument<Position> JsonPatchDocument { get; set; }
    }

    public class PartialUpdateCustomerContactCommandHandler : IRequestHandler<PartialUpdatePositionCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public PartialUpdateCustomerContactCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(PartialUpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.PositionRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            if (request.JsonPatchDocument.Operations.Count == 0) throw new BadHttpRequestException($"Unable to modify element, not operations yet");
            request.JsonPatchDocument.ApplyTo(entity, error => throw new BadHttpRequestException($"Unable to modify element because the fields are invalids ${error.ErrorMessage}"));
            await _unitOfWork.PositionRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");
        }
    }



}
