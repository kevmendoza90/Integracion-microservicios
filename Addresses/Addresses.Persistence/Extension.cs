using Addresses.Core.Interfaces.Repositories;
using Addresses.Persistence.Data;
using Addresses.Persistence.Mapper;
using Forms.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Addresses.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration["sql:cn"]));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MapperProfile>();
            });
            return services;
        }
    }
}
