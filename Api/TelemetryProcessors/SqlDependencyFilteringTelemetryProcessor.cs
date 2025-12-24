using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public sealed class SqlDependencyFilteringTelemetryProcessor : ITelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public SqlDependencyFilteringTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        if (IsSqlDependency(item))
        {
            return; 
        }

        _next.Process(item);
    }

    private static bool IsSqlDependency(ITelemetry item)
    {
        var dependency = item as DependencyTelemetry;
        var dependencyName = dependency?.Name;

        if (string.IsNullOrWhiteSpace(dependencyName))
        {
            return false;
        }

        return dependencyName.Contains("SQL", StringComparison.OrdinalIgnoreCase);
    }
}