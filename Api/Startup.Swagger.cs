using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api;

public partial class Startup
{
    private const string CurrentVersion = "v1";
    private const string ApiName = "SerilogWebApiDemo";
    private static readonly string Url = $"{CurrentVersion}/swagger.json";
    private static readonly OpenApiInfo Info = new() { Title = ApiName, Version = CurrentVersion };

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc(CurrentVersion, Info);
        });
    }

    private static void UseSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DisplayRequestDuration();
            c.SwaggerEndpoint(Url, ApiName);
        });
    }
}