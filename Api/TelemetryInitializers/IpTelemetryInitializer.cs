using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryInitializers;

public class IpTelemetryInitializer : ITelemetryInitializer
{
    private const string IpKey = "IpAddress";

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is MetricTelemetry)
        {
            return;
        }

        telemetry.Context.GlobalProperties.Add(IpKey, FindCurrentIpAddress());
    }

    private static string FindCurrentIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            ?.ToString() ?? "Ip address not found";
    }
}