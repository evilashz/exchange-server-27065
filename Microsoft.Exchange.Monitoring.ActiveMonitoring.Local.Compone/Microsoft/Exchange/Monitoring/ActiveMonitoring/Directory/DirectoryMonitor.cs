using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000139 RID: 313
	public class DirectoryMonitor : OverallXFailuresMonitor
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000374DD File Offset: 0x000356DD
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000374E5 File Offset: 0x000356E5
		public Task<ProbeResult> LastProbeResult { get; private set; }

		// Token: 0x06000930 RID: 2352 RVA: 0x000374F0 File Offset: 0x000356F0
		public static MonitorDefinition CreateMonitor(string targetResource, string monitorName, int recurrenceInterval, int monitoringInterval, int monitoringThreshold, string sampleMask, string assemblyPath, int maxRetryAttempt, TracingContext traceContext)
		{
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.DirectoryTracer, traceContext, "DirectoryDiscovery.CreateMonitor: Creating {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\DirectoryMonitor.cs", 54);
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = assemblyPath;
			monitorDefinition.TypeName = typeof(DirectoryMonitor).FullName;
			monitorDefinition.Name = monitorName;
			monitorDefinition.ServiceName = ExchangeComponent.AD.Name;
			monitorDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.TimeoutSeconds = recurrenceInterval;
			monitorDefinition.MaxRetryAttempts = maxRetryAttempt;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.MonitoringIntervalSeconds = monitoringInterval;
			monitorDefinition.MonitoringThreshold = (double)monitoringThreshold;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = ExchangeComponent.AD;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.DirectoryTracer, traceContext, "DirectoryDiscovery.CreateMonitor: Created {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\DirectoryMonitor.cs", 76);
			return monitorDefinition;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003765C File Offset: 0x0003585C
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			base.DoMonitorWork(cancellationToken);
			this.LastProbeResult = this.GetLastDirectoryProbeResult(cancellationToken);
			this.LastProbeResult.Continue(delegate(ProbeResult lastProbeResult)
			{
				if (lastProbeResult != null)
				{
					string stateAttribute = lastProbeResult.StateAttribute1;
					if (!string.IsNullOrEmpty(stateAttribute))
					{
						base.Result.StateAttribute1 = stateAttribute;
					}
					string stateAttribute2 = lastProbeResult.StateAttribute2;
					if (!string.IsNullOrEmpty(stateAttribute2))
					{
						base.Result.StateAttribute2 = stateAttribute2;
					}
					string stateAttribute3 = lastProbeResult.StateAttribute3;
					if (!string.IsNullOrEmpty(stateAttribute3))
					{
						base.Result.StateAttribute3 = stateAttribute3;
					}
					string stateAttribute4 = lastProbeResult.StateAttribute4;
					if (!string.IsNullOrEmpty(stateAttribute4))
					{
						base.Result.StateAttribute4 = stateAttribute4;
					}
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00037690 File Offset: 0x00035890
		private Task<ProbeResult> GetLastDirectoryProbeResult(CancellationToken cancellationToken)
		{
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, executionStartTime);
			return probeResults.ExecuteAsync(cancellationToken, base.TraceContext);
		}
	}
}
