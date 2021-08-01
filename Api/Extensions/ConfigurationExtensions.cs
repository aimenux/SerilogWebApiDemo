using System.Net.Sockets;
using Microsoft.Extensions.Configuration;

namespace Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetInstrumentationKey(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:2:Args:instrumentationKey"];
        }

        public static string GetFilePath(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:1:Args:path"];
        }

        public static string GetOutputTemplate(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:0:Args:outputTemplate"];
        }

        public static int GetRemotePort(this IConfiguration configuration)
        {
            return configuration.GetValue<int>("Serilog:WriteTo:3:Args:remotePort");
        }

        public static string GetRemoteAddress(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:3:Args:remoteAddress"];
        }

        public static AddressFamily GetAddressFamily(this IConfiguration configuration)
        {
            return configuration.GetValue<AddressFamily>("Serilog:WriteTo:3:Args:family");
        }
    }
}