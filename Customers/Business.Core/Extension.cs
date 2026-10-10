using Business.Core.Config;
using Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Business.Core
{
    public static class Extension
    {
        private const string API_ADDRESSES = "ApiAddresses";
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            services.Configure<ApiAddressesConfig>(configuration.GetSection(API_ADDRESSES));
            services.AddMediatR(Assembly.Load("Business.Core"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.Load("Business.Core"));
            return services;
        }
    }
}
