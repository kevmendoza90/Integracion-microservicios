using Business.Core;
using Business.Persistence;
using Business.Infraestructure;
using Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Audit.WebApi;
using Audit.Core;
using Microsoft.AspNetCore.Http;

namespace Business.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Business.Api", Version = "v1" });
                c.EnableAnnotations();
            });
            services.AddCors();
            services.AddCore();
            services.AddPersistence();
            services.AddInfraestructure(Configuration);
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuditMiddleware(_ => _
            .WithEventType("HttpAction")
            .IncludeHeaders()
            .IncludeRequestBody()
            .IncludeResponseBody());


            Audit.Core.Configuration.Setup()
               .UseFileLogProvider(_ => _
                   .Directory(@"C:\Logs")
                   .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}_{ev.EventType}.json"));

            Audit.Core.Configuration.JsonSettings.WriteIndented = true;

            // Include the trace identifier in the audit events
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                scope.SetCustomField("TraceId", httpContextAccessor.HttpContext?.TraceIdentifier);
            });

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
