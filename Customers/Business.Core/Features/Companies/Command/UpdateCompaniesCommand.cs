using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Common.Attributes;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Companies.Command
{
    [AuditLog]
    public class UpdateCompaniesCommand : CompanyDto, IRequest<HttpResponse<string>>
    {
        
    }
   
    public class UpdateCompaniesCommandHandler : IRequestHandler<UpdateCompaniesCommand, HttpResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public UpdateCompaniesCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<string>> Handle(UpdateCompaniesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CompanyRepository.GetOneByAsync(x => x.Id == request.Id);
            if (entity is null) throw new KeyNotFoundException($"Unable to modify element because an entry with Id: {request.Id} could not be found");
            entity.Name = request.Name;
            entity.Sigla = request.Sigla;
            entity.MainEmail = request.MainEmail;
            entity.IsActive = request.IsActive;
            await _unitOfWork.CompanyRepository.UpdateAsync(entity);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return new HttpResponse<string>(null, $"The Company {entity.Name} has been updated");

        }


    }
}
