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

namespace Addresses.Core.Features.Cities.Query
{
    public class GetCitiesQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<CityDto>>>>
    {
        public string Filter { get; set; }
    }


    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, HttpResponse<PagedResponse<List<CityDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<PagedResponse<List<CityDto>>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CityRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );
            return new HttpResponse<PagedResponse<List<CityDto>>>(new PagedResponse<List<CityDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<CityDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<City, bool>> GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(City), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<City, bool>>)expression;
                return typedExpression;
            }
            catch
            {

                throw new ValidationException("filter expression invalid");
            }
        }

    }

}
