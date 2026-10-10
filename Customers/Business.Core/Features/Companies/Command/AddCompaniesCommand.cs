using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Attributes;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Companies.Command
{
    [AuditLog]
    public class AddCompaniesCommand : IRequest<HttpResponse<CompanyDto>>
    {
        public string Name { get; set; }
        public string Sigla { get; set; }
        public string  MainEmail { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddCompaniesCommandHandler : IRequestHandler<AddCompaniesCommand, HttpResponse<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public AddCompaniesCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<HttpResponse<CompanyDto>> Handle(AddCompaniesCommand request, CancellationToken cancellationToken)
        {
            var questionType = _mapper.Map<Company>(request);
            var result = await _unitOfWork.CompanyRepository.AddAsync(questionType);
            var entity = _mapper.Map<CompanyDto>(result);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CompanyDto>(entity, $"The Company {entity.Name} has been created");
        }
    }
}
