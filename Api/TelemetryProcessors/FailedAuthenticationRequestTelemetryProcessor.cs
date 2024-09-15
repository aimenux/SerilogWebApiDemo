using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public class FailedAuthenticationRequestTelemetryProcessor : ITelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public FailedAuthenticationRequestTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        if (IsAuthenticationFailed(item))
        {
            return; 
        }

        _next.Process(item);
    }

    private static bool IsAuthenticationFailed(ITelemetry item)
    {
        if (item is RequestTelemetry requestTelemetry)
        {
            return string.Equals(requestTelemetry.ResponseCode, "401");
        }

        return false;
    }
}