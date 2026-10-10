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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core.Features.Customers.Query
{
    //public class GetCustomersQuery : RequestParameters { }
    public class GetCustomersQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<CustomerDto>>>>
    {
        public string Filter { get; set; }
    }

    public class GetCustomersQueryHandlerr : IRequestHandler<GetCustomersQuery, HttpResponse<PagedResponse<List<CustomerDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCustomersQueryHandlerr(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HttpResponse<PagedResponse<List<CustomerDto>>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CustomerRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null: GetExpression(request.Filter)
                );
            List<Customer> data = result.Data.ToList();
            return new HttpResponse<PagedResponse<List<CustomerDto>>>(new PagedResponse<List<CustomerDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<CustomerDto>>(data)
            });
        }

        private static Expression<Func<Customer,bool>>GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(Customer), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<Customer, bool>>)expression;
                return typedExpression;
            }
            catch 
            {

                throw new ValidationException("filter expression invalid");
            }
        }
    }
}
