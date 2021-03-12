using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Wascl
{
	// Token: 0x0200053B RID: 1339
	public sealed class WasclDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x000C7FF8 File Offset: 0x000C61F8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WasclTracer, base.TraceContext, "WasclDiscovery.DoWork: Mailbox role is not installed on this server. Wascl maintenance items would not be loaded", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wascl\\WasclDiscovery.cs", 35);
				return;
			}
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WasclTracer, base.TraceContext, "WasclDiscovery.DoWork: Wascl cannot run on non-Datacenter deployments", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wascl\\WasclDiscovery.cs", 41);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.WasclTracer, base.TraceContext, "Wascl.DoWork Discovery Started.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wascl\\WasclDiscovery.cs", 45);
			GenericWorkItemHelper.CreateAllDefinitions(new string[]
			{
				"Wascl.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}
