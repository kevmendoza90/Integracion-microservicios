using Business.Core.Interfaces.Repositories;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Companies.Command
{
    public class DeleteCompanyCommand : IRequest<HttpResponse<string>>
    {
        public int Id { get; set; }
    }
    public class DelteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public DelteCompanyCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }

        public async Task<HttpResponse<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CompanyRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.IsActive = false;
            await _unitOfWork.CompanyRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>(null, $"The Company {entity.Name} has been deactivate");

        }
    }

}
