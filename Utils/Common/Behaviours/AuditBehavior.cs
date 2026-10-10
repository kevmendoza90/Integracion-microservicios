using Common.Attributes;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace Common.Behaviours
{
    public class AuditBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AuditBehavior<TRequest, TResponse>> _logger;

        public AuditBehavior(ILogger<AuditBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var response = await next();

            if (typeof(TRequest).GetCustomAttribute<AuditLogAttribute>() != null)
            {
                _logger.LogInformation("AUDIT {Command}: {Data}",
                    typeof(TRequest).Name, JsonConvert.SerializeObject(request));
            }
            return response;
        }
    }
}