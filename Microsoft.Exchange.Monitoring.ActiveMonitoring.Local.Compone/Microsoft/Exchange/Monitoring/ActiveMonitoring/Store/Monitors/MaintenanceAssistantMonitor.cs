using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Monitors
{
	// Token: 0x020004E2 RID: 1250
	public class MaintenanceAssistantMonitor : NotificationHeartbeatMonitor
	{
		// Token: 0x06001F0B RID: 7947 RVA: 0x000BDA60 File Offset: 0x000BBC60
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			string targetExtension = base.Definition.TargetExtension;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "Starting maintenance assistant check against database {0}", targetExtension, null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\MaintenanceAssistantMonitor.cs", 29);
			Guid databaseGuid = new Guid(targetExtension);
			if (!DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(databaseGuid))
			{
				base.Result.StateAttribute1 = base.Result.ExecutionStartTime.ToString();
				base.Result.IsAlert = false;
				return;
			}
			base.DoMonitorWork(cancellationToken);
		}
	}
}
