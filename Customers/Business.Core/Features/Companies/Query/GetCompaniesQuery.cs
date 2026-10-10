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

namespace Business.Core.Features.Companies.Query
{
    
    public class GetCompaniesQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<CompanyDto>>>>
    {
        public string Filter { get; set; }
        
    }

    public class GetCompaniesQueryHandlerr : IRequestHandler<GetCompaniesQuery, HttpResponse<PagedResponse<List<CompanyDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCompaniesQueryHandlerr(IUnitOfWork unitOfWord, IMapper mapper)
        {
            _unitOfWork = unitOfWord;
            _mapper = mapper;
        }
        public async Task<HttpResponse<PagedResponse<List<CompanyDto>>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CompanyRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter)?null:GetExpression(request.Filter)
                );
            return new HttpResponse<PagedResponse<List<CompanyDto>>>(new PagedResponse<List<CompanyDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize=result.PageSize,
                TotalPage=result.TotalPage,
                TotalRecords=result.TotalRecords,
                Data=_mapper.Map<List<CompanyDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<Company,bool>>GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(Company), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<Company, bool>>)expression;
                return typedExpression;
            }
            catch 
            {

                throw new ValidationException("filter expression invalid");
            }
        }


    }
}
