using Business.Core.Dtos;
using Business.Core.Features.CustomersContacts.Command;
using Business.Core.Features.CustomersContacts.Query;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HttpResponse<int>), 200)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]

        public async Task<HttpResponse<PagedResponse<List<CustomerContactsDto>>>> Get([FromQuery] GetCustomersContacsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(HttpResponse<int>), 201)]
        [ProducesResponseType(typeof(HttpResponse<int>), 500)]
        [ProducesResponseType(typeof(HttpResponse<int>), 400)]

        public async Task<HttpResponse<CustomerContactsDto>> Post([FromBody] AddCustomerContactCommand command)
        {
            return await _mediator.Send(command);
        }


        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Put([FromRoute] int id, [FromBody] UpdateCustomerContactCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        [ProducesResponseType(typeof(HttpResponse<string>), 500)]
        [ProducesResponseType(typeof(HttpResponse<string>), 400)]
        public async Task<HttpResponse<string>> Patch([FromRoute] int id, [FromBody] PartialUpdateCustomerContactCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HttpResponse<int>),200)]
        [ProducesResponseType(typeof(HttpResponse<int>), 204)]
        public async Task<HttpResponse<string>>Delete([FromRoute] int id)
        {
            return await _mediator.Send(new DeleteCustomerContactCommand { Id = id });
        }


    }
}
