using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020002A3 RID: 675
	public sealed class WebUIDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x00048998 File Offset: 0x00046B98
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsWebServiceInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HTTPTracer, WebUIDiscovery.traceContext, "[WebUIDiscovery.DoWork]: WebService role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebUI\\WebUIDiscovery.cs", 42);
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"WebUI.xml"
			}, base.Broker, WebUIDiscovery.traceContext, base.Result);
		}

		// Token: 0x04000AFD RID: 2813
		private static TracingContext traceContext = new TracingContext();
	}
}
