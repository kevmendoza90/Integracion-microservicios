using Addresses.Core.Dtos;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.States.Command
{
    public class PartialUpdateStateCommand : IRequest<HttpResponse<string>>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public JsonPatchDocument<State> JsonPatchDocument { get; set; }
    }
    public class PartialUpdateStateCommandHandler : IRequestHandler<PartialUpdateStateCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public PartialUpdateStateCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<HttpResponse<string>> Handle(PartialUpdateStateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.StateRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            if (request.JsonPatchDocument.Operations.Count == 0) throw new BadHttpRequestException($"Unable to modify element, not operations yet");
            request.JsonPatchDocument.ApplyTo(entity, error => throw new BadHttpRequestException($"Unable to modify element because the fields are invalids ${error.ErrorMessage}"));
            await _unitOfWork.StateRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been updated");
        }

    }
}
