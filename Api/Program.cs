using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                    webHostBuilder.CaptureStartupErrors(true);
                    webHostBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                })
                .UseJsonConfigSerilog();

        public static IHostBuilder UseJsonConfigSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                Serilog.Debugging.SelfLog.Enable(Console.Error);
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithExceptionDetails();
            });
        }

        public static IHostBuilder UseFluentConfigSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                const string key = "Serilog:WriteTo:2:Args:instrumentationKey";
                var instrumentationKey = hostingContext.Configuration.GetValue<string>(key);

                Serilog.Debugging.SelfLog.Enable(Console.Error);
                loggerConfiguration
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces);
            });
        }
    }
}
