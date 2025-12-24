using Api.Extensions;
using Api.Mappers;
using Api.TelemetryInitializers;
using Api.TelemetryProcessors;
using Domain.Ports;
using Domain.Services;
using Infrastructure;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api;

public partial class Startup
{
    private void ConfigureIoc(IServiceCollection services)
    {
        services.AddMemoryCache();

        AddApplicationInsightsTelemetry(services);

        services.AddScoped<IArticleMapper, ArticleMapper>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
    }

    private void AddApplicationInsightsTelemetry(IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry(options =>
        {
            options.ConnectionString = Configuration.GetConnectionString();
        });

        services.AddSingleton<ITelemetryInitializer, IpTelemetryInitializer>();

        services.AddApplicationInsightsTelemetryProcessor<SqlDependencyFilteringTelemetryProcessor>();
        services.AddApplicationInsightsTelemetryProcessor<SlowDependencySamplingTelemetryProcessor>();
        services.AddApplicationInsightsTelemetryProcessor<FastDependencyFilteringTelemetryProcessor>();
        services.AddApplicationInsightsTelemetryProcessor<FailedAuthenticationRequestTelemetryProcessor>();
    }
}