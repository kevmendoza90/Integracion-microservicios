using Business.Core.Interfaces.Repositories;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CustomersContacts.Command
{
    public class DeleteCustomerContactCommand : IRequest<HttpResponse<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteCustomerContactCommandHandler : IRequestHandler<DeleteCustomerContactCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public DeleteCustomerContactCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }

        public async Task<HttpResponse<string>> Handle(DeleteCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CustomerContactRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.IsActive = false;
            await _unitOfWork.CustomerContactRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been deleted");

        }
    }
}
