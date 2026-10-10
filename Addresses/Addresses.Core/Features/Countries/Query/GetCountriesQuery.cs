using Addresses.Core.Dtos;
using Addresses.Core.Dtos.Parameters;
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using AutoMapper;
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

namespace Addresses.Core.Features.Countries.Query
{
    public class GetCountriesQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<CountryDto>>>>
    {
        public string Filter { get; set; }
    }
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, HttpResponse<PagedResponse<List<CountryDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HttpResponse<PagedResponse<List<CountryDto>>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CountryRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );

            return new HttpResponse<PagedResponse<List<CountryDto>>>(new PagedResponse<List<CountryDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<CountryDto>>(result.Data.ToList())
            });
        }
        private static Expression<Func<Country, bool>> GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(Country), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<Country, bool>>)expression;
                return typedExpression;
            }
            catch
            {
                throw new ValidationException("filter expression invalid");
            }
        }

    }
}
