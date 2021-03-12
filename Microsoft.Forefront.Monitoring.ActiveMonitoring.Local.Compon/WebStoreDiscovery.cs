using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000298 RID: 664
	public sealed class WebStoreDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x000471F0 File Offset: 0x000453F0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!FfoLocalEndpointManager.IsWebstoreInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WebStoreTracer, WebStoreDiscovery.traceContext, "[WebStoreDiscovery.DoWork]: Webstore is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebStore\\WebStoreDiscovery.cs", 43);
				base.Result.StateAttribute1 = "WebStoreDiscovery: Webstore is not installed on this server.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"WebStore.xml",
				"FfoWebStore.xml"
			}, base.Broker, base.TraceContext, base.Result);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.WebStoreTracer, WebStoreDiscovery.traceContext, "[WebStoreDiscovery.DoWork]: Webstore work item definitions created.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebStore\\WebStoreDiscovery.cs", 63);
		}

		// Token: 0x04000AC8 RID: 2760
		private static TracingContext traceContext = new TracingContext();
	}
}
