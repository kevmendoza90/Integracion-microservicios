using Addresses.Core.Interfaces.Repositories;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.Countries.Command
{
    public class DeleteCountryCommand : IRequest<HttpResponse<string>>
    {
        public int Id { get; set; }
    }
    public class DelteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public DelteCountryCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }

        public async Task<HttpResponse<string>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CountryRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to delete element because an entry with Id: {request.Id} could not be found");
            entity.IsActive = false;
            await _unitOfWork.CountryRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>("Resource has been deleted");

        }
    }
}
