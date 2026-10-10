using Common.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new HttpResponse<string>
            {
                Succeeded = false,
                ErrorMessage = ex.Message
            };

            switch (ex)
            {
                case ValidationException fluent:   // FluentValidation
                    response.ErrorCode = StatusCodes.Status400BadRequest;
                    response.ErrorMessage = "One or more validation errors occurred";
                    response.ValidationErrors = fluent.Errors.Select(e => e.ErrorMessage).ToList();
                    break;
                case System.ComponentModel.DataAnnotations.ValidationException:
                case BadHttpRequestException:
                    response.ErrorCode = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException:
                    response.ErrorCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    response.ErrorCode = StatusCodes.Status500InternalServerError;
                    _logger.LogError(ex, "Unhandled error");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.ErrorCode;
            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await context.Response.WriteAsync(json);
        }
    }
}