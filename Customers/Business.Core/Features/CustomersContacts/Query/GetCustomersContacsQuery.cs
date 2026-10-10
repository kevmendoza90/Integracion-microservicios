using AutoMapper;
using Business.Core.Dtos;
using Business.Core.Dtos.Parameters;
using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Common.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.CustomersContacts.Query
{
    public class GetCustomersContacsQuery :RequestParameters , IRequest<HttpResponse<PagedResponse<List<CustomerContactsDto>>>>
    {
        public string Filter { get; set; }
        

    }

    public class GetCustomersContacsQueryHandler: IRequestHandler<GetCustomersContacsQuery, HttpResponse<PagedResponse<List<CustomerContactsDto>>>> 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomersContacsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<PagedResponse<List<CustomerContactsDto>>>> Handle(GetCustomersContacsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CustomerContactRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );

            return new HttpResponse<PagedResponse<List<CustomerContactsDto>>>(new PagedResponse<List<CustomerContactsDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<CustomerContactsDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<CustomerContact,bool>> GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(CustomerContact), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<CustomerContact, bool>>)expression;
                return typedExpression;
            }
            catch 
            {

                throw new ValidationException("filter expression invalid");
            }
        }
        
    }
}
