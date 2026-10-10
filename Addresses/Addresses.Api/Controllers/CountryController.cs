using Addresses.Core.Dtos;
using Addresses.Core.Features.Countries.Command;
using Addresses.Core.Features.Countries.Query;
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

    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<PagedResponse<List<CountryDto>>>> Get([FromQuery] GetCountriesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(HttpResponse<int>), 201)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<CountryDto>> Post([FromBody] AddCountriesCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Put([FromRoute] int id, [FromBody] UpdateCountriesCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Patch([FromRoute] int id, [FromBody] PartialUpdateCountriesCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 404)]
        public async Task<HttpResponse<string>> Delete([FromRoute] int id)
        {
            return await _mediator.Send(new DeleteCountryCommand { Id = id });
        }
    }
}
