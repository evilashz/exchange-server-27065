using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Monitors
{
	// Token: 0x020004E0 RID: 1248
	public class DatabaseRPCLatencyMonitor : MonitorWorkItem
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x000BD5D4 File Offset: 0x000BB7D4
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			string targetExtension = base.Definition.TargetExtension;
			base.Result.StateAttribute1 = targetExtension;
			base.Result.StateAttribute2 = base.Definition.TargetResource;
			Guid databaseGuid;
			if (Guid.TryParse(targetExtension, out databaseGuid))
			{
				base.Result.StateAttribute4 = (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(databaseGuid) ? bool.TrueString : bool.FalseString);
			}
			ProbeResult probeResult = this.TryFindEdsTriggerResult(cancellationToken);
			if (probeResult != null)
			{
				if (probeResult.ResultType == ResultType.Failed)
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "Found issues during RPC average latency validation of database {0}, Turning monitor red so responder can do further evaluation", base.Result.StateAttribute2, null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\DatabaseRPCLatencyMonitor.cs", 50);
					base.Result.IsAlert = true;
					base.Result.LastFailedProbeId = probeResult.WorkItemId;
					base.Result.LastFailedProbeResultId = probeResult.ResultId;
					return;
				}
				if (string.Compare(probeResult.Error, "SuppressDatabaseRPCLatencyMonitor", true) == 0)
				{
					base.Result.StateAttribute3 = Strings.DatabaseRPCLatencyMonitorGreenMessage;
				}
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "Found no issues during RPC average latency validation of database {0}", base.Result.StateAttribute2, null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\DatabaseRPCLatencyMonitor.cs", 72);
			base.Result.IsAlert = false;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000BD714 File Offset: 0x000BB914
		private ProbeResult TryFindEdsTriggerResult(CancellationToken cancellationToken)
		{
			string sampleMask = base.Definition.SampleMask;
			return WorkItemResultHelper.GetLastProbeResult(sampleMask, base.Broker, base.MonitoringWindowStartTime, base.Result.ExecutionStartTime, cancellationToken);
		}
	}
}
