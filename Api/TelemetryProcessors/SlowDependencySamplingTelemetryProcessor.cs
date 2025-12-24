using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public sealed class SlowDependencySamplingTelemetryProcessor : ITelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public SlowDependencySamplingTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        if (IsSlowDependency(item))
        {
            ((ISupportSampling)item).SamplingPercentage = 100;
        }

        _next.Process(item);
    }

    private static bool IsSlowDependency(ITelemetry item)
    {
        if (item is DependencyTelemetry dependencyTelemetry)
        {
            return dependencyTelemetry.Duration > TimeSpan.FromMilliseconds(500);
        }

        return false;
    }
}