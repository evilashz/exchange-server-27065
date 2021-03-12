using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200009C RID: 156
	public sealed class MonitoringDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0001A980 File Offset: 0x00018B80
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMonitoringRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, MonitoringDiscovery.traceContext, "[MonitoringDiscovery.DoWork]: Monitoring role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Monitoring\\MonitoringDiscovery.cs", 43);
			}
		}

		// Token: 0x0400027A RID: 634
		private static TracingContext traceContext = new TracingContext();
	}
}
