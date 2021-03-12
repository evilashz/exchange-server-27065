using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000028 RID: 40
	public sealed class CmdletDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00009E48 File Offset: 0x00008048
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CmdletTracer, CmdletDiscovery.traceContext, "[CmdletDiscovery.DoWork]: Bridgehead role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Cmdlet\\CmdletDiscovery.cs", 43);
			}
		}

		// Token: 0x040000D0 RID: 208
		private static TracingContext traceContext = new TracingContext();
	}
}
