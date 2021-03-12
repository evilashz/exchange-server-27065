using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020001CD RID: 461
	public class HeartbeatMonitor : MonitorWorkItem
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x000550D0 File Offset: 0x000532D0
		public static MonitorDefinition CreateDefinition(string monitorName, string probeName)
		{
			return new MonitorDefinition
			{
				AssemblyPath = HeartbeatMonitor.AssemblyPath,
				TypeName = HeartbeatMonitor.TypeName,
				Component = ExchangeComponent.Monitoring,
				ServiceName = ExchangeComponent.Monitoring.Name,
				RecurrenceIntervalSeconds = 0,
				TimeoutSeconds = 30,
				MonitoringIntervalSeconds = 600,
				MaxRetryAttempts = 0,
				Enabled = true,
				Name = monitorName,
				SampleMask = probeName
			};
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000551B8 File Offset: 0x000533B8
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HeartbeatTracer, base.TraceContext, "HeartbeatMonitor.DoWork: Getting health state for all instances of probe '{0}'...", base.Definition.SampleMask, null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\HeartbeatMonitor.cs", 75);
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			IEnumerable<int> query = from p in base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, executionStartTime)
			where p.ResultType == ResultType.Failed
			select p.ResultId;
			Task<int> task = base.Broker.AsDataAccessQuery<int>(query).ExecuteAsync(delegate(int r)
			{
			}, cancellationToken, base.TraceContext);
			task.Continue(delegate(int count)
			{
				WTFDiagnostics.TraceInformation<int, string>(ExTraceGlobals.HeartbeatTracer, base.TraceContext, "[HeartbeatMonitor.DoWork]: Retrieved health state for {0} probe instances of type '{1}'.", count, base.Definition.SampleMask, null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\HeartbeatMonitor.cs", 95);
				base.Result.TotalValue = (double)count;
				if (count != 0)
				{
					base.Result.IsAlert = true;
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0400099F RID: 2463
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040009A0 RID: 2464
		private static readonly string TypeName = typeof(HeartbeatMonitor).FullName;
	}
}
