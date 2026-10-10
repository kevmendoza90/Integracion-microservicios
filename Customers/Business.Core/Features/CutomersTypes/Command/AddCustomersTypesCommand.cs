using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CutomersTypes.Command
{
    public class AddCustomersTypesCommand: IRequest<HttpResponse<CustomerTypeDto>>
    {
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        
    }

    public class AddCustomersTypesCommandHandler : IRequestHandler<AddCustomersTypesCommand,HttpResponse<CustomerTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        public AddCustomersTypesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accesor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accesor;

        }
        public async Task<HttpResponse<CustomerTypeDto>> Handle(AddCustomersTypesCommand request, CancellationToken cancellationToken)
            
        {
            var questionType = _mapper.Map<CustomerType>(request);
            var result = await _unitOfWork.CustomerTypeRepository.AddAsync(questionType);
            var entity = _mapper.Map<CustomerTypeDto>(result);
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new HttpResponse<CustomerTypeDto>(entity);
        }

        
    }
}
