using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200025A RID: 602
	public sealed class StreamingOpticsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x0003B860 File Offset: 0x00039A60
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (FfoLocalEndpointManager.IsHubTransportRoleInstalled)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new List<string>
				{
					"StreamingOptics.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
			if (FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new List<string>
				{
					"StreamingOptics.xml",
					"WS_TblApp.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
			if (!FfoLocalEndpointManager.IsHubTransportRoleInstalled && !FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.AntiSpamTracer, StreamingOpticsDiscovery.traceContext, "[StreamingOpticsDiscovery.DoWork]: Neither HubTransport nor WebService role is installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\StreamingOptics\\StreamingOpticsDiscovery.cs", 58);
				base.Result.StateAttribute1 = "StreamingOpticsDiscovery: Neither HubTransport nor WebService role is installed on this server.";
			}
		}

		// Token: 0x040009C0 RID: 2496
		private static readonly TracingContext traceContext = new TracingContext();
	}
}
