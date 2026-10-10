
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Core.Features.States.Query
{
    public class GetStateQuery : RequestParameters, IRequest<HttpResponse<PagedResponse<List<StateDto>>>>
    {
        public string Filter { get; set; }
    }


    public class GetStateQueryHandler : IRequestHandler<GetStateQuery, HttpResponse<PagedResponse<List<StateDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetStateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<PagedResponse<List<StateDto>>>> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.StateRepository.GetPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                string.IsNullOrEmpty(request.Filter) ? null : GetExpression(request.Filter)
                );
            return new HttpResponse<PagedResponse<List<StateDto>>>(new PagedResponse<List<StateDto>>()
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPage = result.TotalPage,
                TotalRecords = result.TotalRecords,
                Data = _mapper.Map<List<StateDto>>(result.Data.ToList())
            });
        }

        private static Expression<Func<State, bool>> GetExpression(string query)
        {
            try
            {
                var parameters = Expression.Parameter(typeof(State), "x");
                var expression = (Expression)DynamicExpressionParser.ParseLambda(new[] { parameters }, null, query);
                var typedExpression = (Expression<Func<State, bool>>)expression;
                return typedExpression;
            }
            catch
            {

                throw new ValidationException("filter expression invalid");
            }
        }

    }

}
