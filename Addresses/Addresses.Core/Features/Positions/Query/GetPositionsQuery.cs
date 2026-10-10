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

namespace Addresses.Core.Features.Positions.Query
{
    public class GetPositionsQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<PositionDto>>>>
    {
        public string Filter { get; set; }
    }
    public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, HttpResponse<PagedResponse<List<PositionDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPositionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<PagedResponse<List<PositionDto>>>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PositionRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );

            return new HttpResponse<PagedResponse<List<PositionDto>>>(new PagedResponse<List<PositionDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<PositionDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<Position, bool>> GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(Position), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<Position, bool>>)expression;
                return typedExpression;
            }
            catch
            {

                throw new ValidationException("filter expression invalid");
            }
        }
        
    }
}
