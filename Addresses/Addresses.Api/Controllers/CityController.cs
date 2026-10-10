using Addresses.Core.Dtos;
using Addresses.Core.Features.Cities.Command;
using Addresses.Core.Features.Cities.Query;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Addresses.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        
        public async Task<HttpResponse<PagedResponse<List<CityDto>>>> Get([FromQuery] GetCitiesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(HttpResponse<int>), 201)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        
        public async Task<HttpResponse<CityDto>> Post([FromBody] AddCitiesCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Put([FromRoute] int id, [FromBody] UpdateCitiesCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Patch([FromRoute] int id, [FromBody] PartialUpdateCitiesCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 404)]
        public async Task<HttpResponse<string>> Delete([FromRoute] int id)
        {
            return await _mediator.Send(new DeleteCityCommand { Id = id });
        }
    }
}
