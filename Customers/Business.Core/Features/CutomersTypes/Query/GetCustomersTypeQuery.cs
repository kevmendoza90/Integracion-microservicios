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

namespace Business.Core.Features.CutomersTypes.Query
{
    public class GetCustomersTypeQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<CustomerTypeDto>>>>
    {
        public string Filter { get; set; }

    }

    public class GetCustomerTypeQueryHandler : IRequestHandler<GetCustomersTypeQuery,HttpResponse<PagedResponse<List<CustomerTypeDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCustomerTypeQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HttpResponse<PagedResponse<List<CustomerTypeDto>>>> Handle(GetCustomersTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CustomerTypeRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );

            return new HttpResponse<PagedResponse<List<CustomerTypeDto>>>(new PagedResponse<List<CustomerTypeDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<CustomerTypeDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<CustomerType,bool>>GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(CustomerType), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<CustomerType, bool>>)expression;
                return typedExpression;
            }
            catch 
            {

                throw new ValidationException("filter expression invalid");
            }
        }
        

    }
}
