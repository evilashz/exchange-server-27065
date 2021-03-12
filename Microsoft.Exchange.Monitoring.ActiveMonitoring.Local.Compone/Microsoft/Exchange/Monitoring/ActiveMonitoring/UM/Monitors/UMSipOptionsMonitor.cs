using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM.Monitors
{
	// Token: 0x020004B4 RID: 1204
	internal class UMSipOptionsMonitor : OverallConsecutiveProbeFailuresMonitor
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000B5A57 File Offset: 0x000B3C57
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x000B5A5F File Offset: 0x000B3C5F
		public Task<ProbeResult> LastProbeResult { get; private set; }

		// Token: 0x06001E0E RID: 7694 RVA: 0x000B5A68 File Offset: 0x000B3C68
		public static MonitorDefinition CreateUMSipOptionMonitor(string targetResource, Component exchangeComponent, string monitorName, string sampleMask, int recurrenceIntervalSeconds, int timeoutSeconds, int maxRetryAttempts, int monitoringIntervalSeconds, int monitoringThreshold, TracingContext traceContext)
		{
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.UnifiedMessagingTracer, traceContext, "UMDiscovery:: DoWork(): Creating {0} for {1}", monitorName, targetResource, null, "CreateUMSipOptionMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\Monitors\\UMSipOptionsMonitor.cs", 57);
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = UMMonitoringConstants.AssemblyPath;
			monitorDefinition.TypeName = typeof(UMSipOptionsMonitor).FullName;
			monitorDefinition.ServiceName = exchangeComponent.Name;
			monitorDefinition.Name = monitorName;
			monitorDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.TimeoutSeconds = timeoutSeconds;
			monitorDefinition.MaxRetryAttempts = maxRetryAttempts;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.MonitoringIntervalSeconds = monitoringIntervalSeconds;
			monitorDefinition.MonitoringThreshold = (double)monitoringThreshold;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = exchangeComponent;
			if (exchangeComponent.Equals(ExchangeComponent.UMCallRouter))
			{
				monitorDefinition.IsHaImpacting = true;
			}
			else
			{
				monitorDefinition.IsHaImpacting = false;
			}
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.UnifiedMessagingTracer, traceContext, "UMDiscovery:: DoWork(): Created {0} for {1}", monitorName, targetResource, null, "CreateUMSipOptionMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\Monitors\\UMSipOptionsMonitor.cs", 90);
			return monitorDefinition;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000B5B8F File Offset: 0x000B3D8F
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			base.DoMonitorWork(cancellationToken);
			this.LastProbeResult = this.GetLastUMSipOptionProbeResult(cancellationToken);
			this.LastProbeResult.Continue(delegate(ProbeResult lastProbeResult)
			{
				if (lastProbeResult != null && lastProbeResult.StateAttribute1 != null)
				{
					base.Result.StateAttribute1 = lastProbeResult.StateAttribute1;
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000B5BC4 File Offset: 0x000B3DC4
		private Task<ProbeResult> GetLastUMSipOptionProbeResult(CancellationToken cancellationToken)
		{
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, executionStartTime);
			return probeResults.ExecuteAsync(cancellationToken, base.TraceContext);
		}
	}
}
