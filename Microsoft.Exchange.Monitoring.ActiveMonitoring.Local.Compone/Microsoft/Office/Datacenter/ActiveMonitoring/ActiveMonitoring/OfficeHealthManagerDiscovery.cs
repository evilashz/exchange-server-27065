using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.ActiveMonitoring
{
	// Token: 0x020001DB RID: 475
	public sealed class OfficeHealthManagerDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00058334 File Offset: 0x00056534
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding healthstate collection monitor", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\OfficeHealthManagerDiscovery.cs", 34);
			MonitorDefinition definition = HealthStateCollectionMonitor.CreateDefinition("ServerHealthStateCollectionMonitor");
			base.Broker.AddWorkDefinition<MonitorDefinition>(definition, base.TraceContext);
		}

		// Token: 0x040009E6 RID: 2534
		private const string HealthStateCollectionMonitorName = "ServerHealthStateCollectionMonitor";
	}
}
