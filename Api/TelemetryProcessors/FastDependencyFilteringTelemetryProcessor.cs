using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors
{
    public class FastDependencyFilteringTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        public FastDependencyFilteringTelemetryProcessor(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            if (IsFastDependency(item))
            {
                return; 
            }

            _next.Process(item);
        }

        private static bool IsFastDependency(ITelemetry item)
        {
            if (item is DependencyTelemetry dependencyTelemetry)
            {
                return dependencyTelemetry.Duration < TimeSpan.FromMilliseconds(100);
            }

            return false;
        }
    }
}
