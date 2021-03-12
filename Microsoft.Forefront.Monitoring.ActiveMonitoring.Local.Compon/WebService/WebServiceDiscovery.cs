using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000294 RID: 660
	public sealed class WebServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x00046DCC File Offset: 0x00044FCC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WebServiceTracer, WebServiceDiscovery.traceContext, "[WebServiceDiscovery.DoWork]: WebService role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\WebServiceDiscovery.cs", 44);
				base.Result.StateAttribute1 = "WebServiceDiscovery: WebService role is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"UMCWebService.xml",
				"DALWebService.xml",
				"RusPublisherWeb.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}

		// Token: 0x04000ABF RID: 2751
		private const string EscalationTeam = "FFO Web Service";

		// Token: 0x04000AC0 RID: 2752
		private static TracingContext traceContext = new TracingContext();
	}
}
