using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Udp.TextFormatters;

namespace Api.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseRandomConfigSerilog(this IHostBuilder builder)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var random = new Random(seed);
            var randomValue = random.Next(seed, int.MaxValue);
            return randomValue % 2 == 0 
                ? builder.UseJsonConfigSerilog() 
                : builder.UseFluentConfigSerilog();
        }

        public static IHostBuilder UseJsonConfigSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                SelfLog.Enable(Console.Error);

                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
            });
        }

        public static IHostBuilder UseFluentConfigSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var remotePort = hostingContext.Configuration.GetRemotePort();
                var remoteAddress = hostingContext.Configuration.GetRemoteAddress();
                var addressFamily = hostingContext.Configuration.GetAddressFamily();
                var outputTemplate = hostingContext.Configuration.GetOutputTemplate();
                var instrumentationKey = hostingContext.Configuration.GetInstrumentationKey();

                SelfLog.Enable(Console.Error);

                loggerConfiguration
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces)
                    .WriteTo.Console(outputTemplate: outputTemplate)
                    .WriteTo.Udp(remoteAddress, remotePort, addressFamily, new Log4jTextFormatter());
            });
        }
    }
}
