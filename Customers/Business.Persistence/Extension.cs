using Business.Core.Interfaces.Repositories;
using Business.Persistence.Data;
using Business.Persistence.Mapper;
using Business.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Audit.Core;
using MediatR;
using Common.Behaviours;

namespace Business.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration["sql:cn"]));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MapperProfile>();
            });
            return services;
        }
    }
}
