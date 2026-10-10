using Addresses.Core.Dtos;
using Addresses.Core.Features.Positions.Command;
using Addresses.Core.Features.Positions.Query;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]

        public async Task<HttpResponse<PagedResponse<List<PositionDto>>>> Get([FromQuery] GetPositionsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(HttpResponse<int>), 201)]
        [ProducesResponseType(typeof(HttpResponse<int>), 500)]
        [ProducesResponseType(typeof(HttpResponse<int>), 400)]

        public async Task<HttpResponse<PositionDto>> Post([FromBody] AddPositionCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Put([FromRoute] int id, [FromBody] UpdatePositionCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Patch([FromRoute] int id, [FromBody] PartialUpdatePositionCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        public async Task<HttpResponse<string>> Delete([FromRoute] int id)
        {
            return await _mediator.Send(new DeletePositionCommand { Id = id });
        }
    }
}
