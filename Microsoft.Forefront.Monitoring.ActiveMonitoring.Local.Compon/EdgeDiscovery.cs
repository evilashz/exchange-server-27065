using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000266 RID: 614
	public sealed class EdgeDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x0003C578 File Offset: 0x0003A778
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "EdgeDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\Discovery\\EdgeDiscovery.cs", 45);
			if (!DiscoveryUtils.IsGatewayRoleInstalled())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "EdgeDiscovery.DoWork(): Edge role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\Discovery\\EdgeDiscovery.cs", 49);
				base.Result.StateAttribute1 = "EdgeDiscovery: Edge role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"Core_Edge.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}

		// Token: 0x040009CC RID: 2508
		private const string ServiceName = "Transport";
	}
}
