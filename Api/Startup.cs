using Api.Filters;
using Api.Mappers;
using Api.TelemetryInitializers;
using Api.TelemetryProcessors;
using Domain.Ports;
using Domain.Services;
using Infrastructure;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        private const string ApiVersion = "V1";
        private const string ApiName = "SerilogWebApiDemo";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string InstrumentationKey => Configuration["Serilog:WriteTo:2:Args:instrumentationKey"];

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            });

            services.AddMemoryCache();

            services.AddApplicationInsightsTelemetry(InstrumentationKey);

            services.AddSingleton<ITelemetryInitializer, IpTelemetryInitializer>();

            services.AddSingleton<IArticleMapper, ArticleMapper>();
            services.AddSingleton<IArticleService, ArticleService>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();

            services.AddApplicationInsightsTelemetryProcessor<SqlDependencyFilteringTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<SlowDependencySamplingTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<FastDependencyFilteringTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<FailedAuthenticationRequestTelemetryProcessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiName, Version = ApiVersion });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
