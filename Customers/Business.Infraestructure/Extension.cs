using Business.Core.Interfaces.Services;
using Business.Infraestructure.Mapper;
using Business.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NugetPackage_Rest.Builders;
using NugetPackage_Rest.Extensions;
using NugetPackage_Rest.Interfaces.IServices;

namespace Business.Infraestructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. Enlaza "RestSettings" del appsettings con IOptions<RequestSettings>
            services.AddRequestLogging(configuration);

            // 2. Registra IRest como typed client de IHttpClientFactory
            services.AddHttpClient<IRest, RestBuilder>();

            // 3. Servicios que consumen otros microservicios
            services.AddScoped<IAddressService, AddressService>();

            services.AddAutoMapper(config => config.AddProfile<MapperProfile>());
            return services;
        }
    }
}