using System;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000AC RID: 172
	public class ComponentHealthHeartbeatMonitor : ComponentHealthMonitor
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x000237EC File Offset: 0x000219EC
		public static MonitorDefinition CreateDefinition(Component component, int recurrenceIntervalSeconds, int timeoutSeconds, int maxRetryAttempts, int monitoringIntervalSeconds, double monitoringThreshold)
		{
			return new MonitorDefinition
			{
				AssemblyPath = ComponentHealthMonitor.AssemblyPath,
				TypeName = typeof(ComponentHealthHeartbeatMonitor).FullName,
				Name = string.Format("{0}: {1}", typeof(ComponentHealthHeartbeatMonitor).Name, component.ToString()),
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = recurrenceIntervalSeconds / 2,
				MaxRetryAttempts = maxRetryAttempts,
				MonitoringIntervalSeconds = monitoringIntervalSeconds,
				TargetResource = Environment.MachineName,
				Component = component,
				MonitoringThreshold = monitoringThreshold,
				SampleMask = "*"
			};
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0002388C File Offset: 0x00021A8C
		protected override void AnalyzeComponentMonitorResults(int totalResults, int failedResults, string errorContent)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "{0}: Starting analyzing component results...", base.Definition.Name, null, "AnalyzeComponentMonitorResults", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\ComponentHealthHeartbeatMonitor.cs", 85);
			if ((double)totalResults < base.Definition.MonitoringThreshold)
			{
				base.Result.IsAlert = true;
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "{0}: Result.IsAlert set to true.", base.Definition.Name, null, "AnalyzeComponentMonitorResults", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\ComponentHealthHeartbeatMonitor.cs", 96);
			}
			else
			{
				base.Result.IsAlert = false;
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "{0}: Result.IsAlert set to false.", base.Definition.Name, null, "AnalyzeComponentMonitorResults", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\ComponentHealthHeartbeatMonitor.cs", 106);
			}
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "{0}: Finished analyzing component results.", base.Definition.Name, null, "AnalyzeComponentMonitorResults", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\ComponentHealthHeartbeatMonitor.cs", 113);
		}
	}
}
