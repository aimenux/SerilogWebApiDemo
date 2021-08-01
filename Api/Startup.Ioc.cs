using Api.Extensions;
using Api.Mappers;
using Api.TelemetryInitializers;
using Api.TelemetryProcessors;
using Domain.Ports;
using Domain.Services;
using Infrastructure;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public partial class Startup
    {
        private void ConfigureIoc(IServiceCollection services)
        {
            services.AddMemoryCache();

            AddApplicationInsightsTelemetry(services);

            services.AddSingleton<IArticleMapper, ArticleMapper>();
            services.AddSingleton<IArticleService, ArticleService>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();
        }

        private void AddApplicationInsightsTelemetry(IServiceCollection services)
        {
            var instrumentationKey = Configuration.GetInstrumentationKey();

            services.AddApplicationInsightsTelemetry(instrumentationKey);

            services.AddSingleton<ITelemetryInitializer, IpTelemetryInitializer>();

            services.AddApplicationInsightsTelemetryProcessor<SqlDependencyFilteringTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<SlowDependencySamplingTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<FastDependencyFilteringTelemetryProcessor>();
            services.AddApplicationInsightsTelemetryProcessor<FailedAuthenticationRequestTelemetryProcessor>();
        }
    }
}
